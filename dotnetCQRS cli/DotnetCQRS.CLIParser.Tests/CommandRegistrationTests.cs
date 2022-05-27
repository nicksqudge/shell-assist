using DotnetCQRS.Commands;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using DotnetCQRS.Extensions.FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace DotnetCQRS.CLIParser.Tests
{
    public class CommandRegistrationTests
    {
        private CliParser cliParser;

        private void Arrange()
        {
            var commandDispatcher = Substitute.For<ICommandDispatcher>();
            commandDispatcher.RunAsync(Arg.Any<SimpleCommand>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(Result.Success());            

            cliParser = new CliParser(commandDispatcher)
                .AddCommand<SimpleCommand>("simple");
        }

        private Task<TestCliParserResult> Act(string command)
        {
            return cliParser.TestParseAsync(command, CancellationToken.None);
        }

        [Fact]
        public async Task MatchedCommandsShouldRunHandler()
        {
            Arrange();

            var result = await Act("simple");

            result.Command.Should().BeOfType<SimpleCommand>();
            result.RanHandler.Should().BeTrue();
            result.RanHelp.Should().BeFalse();
            result.Result.Should().BeSuccess();
        }

        [Theory]
        [InlineData("--help")]
        [InlineData("-?")]
        [InlineData("-h")]
        public async Task CommandWithHelpShouldRunHelpHandler(string helpCommand)
        {
            Arrange();

            var result = await Act($"simple {helpCommand}");

            result.Command.Should().BeOfType<SimpleCommand>();
            result.RanHandler.Should().BeFalse();
            result.RanHelp.Should().BeTrue();
            result.Result.Should().BeSuccess();
        }

        [Fact]
        public async Task UnmatchedCommandsShouldReturnAnError()
        {
            Arrange();

            var result = await Act("unknown");

            result.Command.Should().BeNull();
            result.RanHandler.Should().BeFalse();
            result.RanHelp.Should().BeFalse();
            result.Result.Should().BeFailure()
                .And.HaveErrorCode("command_not_found");
        }

        [Fact]
        public async Task PassNothing()
        {
            Arrange();

            var result = await Act("");

            result.Command.Should().BeNull();
            result.RanHandler.Should().BeFalse();
            result.RanHelp.Should().BeFalse();
            result.Result.Should().BeFailure()
                .And.HaveErrorCode("invalid_args");
        }
    }

    internal class SimpleCommand : ICommand
    {

    }
}
