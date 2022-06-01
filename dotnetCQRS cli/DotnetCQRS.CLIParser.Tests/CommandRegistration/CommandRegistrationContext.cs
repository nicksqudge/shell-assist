using DotnetCQRS.CLIParser.Tests.Helpers;
using DotnetCQRS.Commands;
using DotnetCQRS.Extensions.FluentAssertions;
using FluentAssertions;
using NSubstitute;

namespace DotnetCQRS.CLIParser.Tests.CommandRegistration
{
    public class CommandRegistrationContext
    {
        private readonly CliParser _cliParser;
        private readonly ICommandDispatcher _commandDispatcher;

        public CommandRegistrationContext()
        {
            _commandDispatcher = Substitute.For<ICommandDispatcher>();

            _cliParser = new CliParser(_commandDispatcher)
                .AddCommand<SimpleCommand>("simple");
        }

        public CommandRegistrationContext HandlerReturnsSuccess()
        {
            _commandDispatcher
                .RunAsync(Arg.Any<SimpleCommand>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(Result.Success());

            return this;
        }

        public Task<TestCliParserResult> Run(string input)
        {
            return _cliParser.TestParseAsync(input, CancellationToken.None);
        }
    }
}
