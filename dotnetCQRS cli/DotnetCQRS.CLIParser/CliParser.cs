using DotnetCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.CLIParser
{
    public class CliParser
    {
        private readonly Dictionary<string, ICommand> map = new Dictionary<string, ICommand>();
        private readonly ICommandDispatcher commandDispatcher;
        private Action<ICommand> identifiedCommand;
        private Action<bool> ranHandler;

        public CliParser(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public CliParser AddCommand(string keyword, ICommand commandType)
        {
            if (this.map.ContainsKey(keyword))
                throw new Exception($"Command {keyword} already exists");

            this.map.Add(keyword, commandType);
            return this;
        }

        public async Task<TestCliParserResult> TestParseAsync(string[] args, CancellationToken cancellationToken)
        {
            var result = new TestCliParserResult();

            identifiedCommand = (command) => result.Command = command;
            ranHandler = (ranHandler) => result.RanHandler = ranHandler;

            result.Result = await ParseAsync(args, cancellationToken);

            return result;            
        }

        public async Task<Result> ParseAsync(string[] args, CancellationToken cancellationToken)
        {
            if (args == null || !args.Any())
                return new Result(false, "invalid_args");

            string keyword = args.First().Trim();
            if (!map.ContainsKey(keyword))
                return new Result(false, "command_not_found");

            var command = map[keyword];
            identifiedCommand?.Invoke(command);

            var result = await this.commandDispatcher.RunAsync(command, cancellationToken);
            ranHandler?.Invoke(true);
            return result;
        }
    }
}
