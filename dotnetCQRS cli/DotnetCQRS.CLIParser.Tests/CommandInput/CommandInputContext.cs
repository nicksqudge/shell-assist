using DotnetCQRS.CLIParser.Tests.Helpers;
using DotnetCQRS.Commands;
using FluentAssertions;
using NSubstitute;

namespace DotnetCQRS.CLIParser.Tests.CommandInput;

public class CommandInputContext
{
    private readonly CliParser _cliParser;
    private readonly ICommandDispatcher _commandDispatcher;
    private static CommandDetail<InputTestCommand> _getDetailResponse;

    public class InputTestCommand : ICliCommand
    {
        public string Parameter { get; set; }
        public int Argument { get; set; }
        public bool Flag { get; set; }

        public CommandDetail<InputTestCommand> GetDetail()
            => CommandInputContext._getDetailResponse;
    }

    public CommandInputContext SetCommandDetail(CommandDetail<InputTestCommand> input)
    {
        _getDetailResponse = input;
        return this;
    }

    public CommandInputContext()
    {
        _commandDispatcher = Substitute.For<ICommandDispatcher>();
        
        _cliParser = new CliParser(_commandDispatcher)
            .AddCommand<InputTestCommand>("simple");
    }

    public Task<TestCliParserResult> Run(string input)
    {
        return _cliParser.TestParseAsync(input, CancellationToken.None);
    }
}