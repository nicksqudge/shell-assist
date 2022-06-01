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
        private readonly Dictionary<string, ICliCommand> map = new Dictionary<string, ICliCommand>();
        private readonly ICommandDispatcher commandDispatcher;
        private readonly string[] helpKeys = new string[]
        {
            "-h", "--help", "-?"
        };
        private Action<ICliCommand> identifiedCommand;
        private Action<bool> ranHandler;
        private Action<bool> ranHelp;

        public CliParser(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public CliParser AddCommand<T>(string keyword, T commandType)
            where T : class, ICliCommand<T>
        {
            if (map.ContainsKey(keyword))
                throw new Exception($"Command {keyword} already exists");

            map.Add(keyword, commandType);
            return this;
        }

        public async Task<TestCliParserResult> TestParseAsync(string[] args, CancellationToken cancellationToken)
        {
            var result = new TestCliParserResult();

            identifiedCommand = (command) => result.Command = command;
            ranHandler = (ranHandler) => result.RanHandler = ranHandler;
            ranHelp = (ranHelp) => result.RanHelp = ranHelp;

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
            
            var command = ProcessCommand(args.RemoveCommandKeyword(), map[keyword]);
            identifiedCommand?.Invoke(command);

            if (helpKeys.Contains(args.Last().Trim()))
            {
                ranHelp?.Invoke(true);
                ranHandler?.Invoke(false);
                return Result.Success();
            }
            else
            {
                var result = await this.commandDispatcher.RunAsync(command, cancellationToken);
                ranHandler?.Invoke(true);
                ranHelp?.Invoke(false);
                return result;
            }
        }

        private ICliCommand ProcessCommand(string[] args, ICliCommand command)
        {
            var definition = command.GetDetail();
            
            
            return command;
        }
    }
}
