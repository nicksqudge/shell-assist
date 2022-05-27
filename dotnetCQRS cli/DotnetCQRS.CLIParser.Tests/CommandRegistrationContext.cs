using DotnetCQRS.Commands;
using FluentAssertions;
using DotnetCQRS.Extensions.FluentAssertions;
using NSubstitute;
using DotnetCQRS.CLIParser.Tests.Helpers;

namespace DotnetCQRS.CLIParser.Tests
{
    public class CommandRegistrationContext : ITestContext<string>
    {
        private CliParser cliParser;
        private ICommandDispatcher commandDispatcher;
        private TestCliParserResult result;

        public CommandRegistrationContext()
        {
            commandDispatcher = Substitute.For<ICommandDispatcher>();

            cliParser = new CliParser(commandDispatcher)
                .AddCommand<SimpleCommand>("simple");
        }

        public async Task Act(string input)
        {
            result = await cliParser.TestParseAsync(input, CancellationToken.None);
        }

        public CommandRegistrationContext HandlerReturnsSuccess()
        {
            commandDispatcher
                .RunAsync(Arg.Any<SimpleCommand>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(Result.Success());

            return this;
        }

        public CommandRegistrationContext ShouldBeSimpleCommand()
        {
            result.Command.Should().BeOfType<SimpleCommand>();
            return this;
        }

        public CommandRegistrationContext ShouldHaveNoCommand()
        {
            result.Command.Should().BeNull();
            return this;
        }

        public CommandRegistrationContext ShouldHaveRanHandler()
        {
            result.RanHandler.Should().BeTrue();
            return this;
        }

        public CommandRegistrationContext ShouldNotHaveRanHandler()
        {
            result.RanHandler.Should().BeFalse();
            return this;
        }

        public CommandRegistrationContext ShouldHaveRanHelp()
        {
            result.RanHelp.Should().BeTrue();
            return this;
        }

        public CommandRegistrationContext ShouldNotHaveRanHelp()
        {
            result.RanHelp.Should().BeFalse();
            return this;
        }

        public CommandRegistrationContext ShouldHaveSuccessResult()
        {
            result.Result.Should().BeSuccess();
            return this;
        }

        public CommandRegistrationContext ShouldHaveFailedResult()
        {
            result.Result.Should().BeFailure();
            return this;
        }

        public CommandRegistrationContext ShouldHaveErrorCode(string code)
        {
            result.Result.Should().HaveErrorCode(code);
            return this;
        }
    }
}
