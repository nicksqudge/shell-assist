using DotnetCQRS.CLIParser.Tests.Helpers;
using FluentAssertions;


namespace DotnetCQRS.CLIParser.Tests.CommandRegistration
{
    public class CommandRegistrationTests : ArrangeActAssert
    {
        [Fact]
        public async Task MatchedCommandsShouldRunHandler()
        {
            await Arrange(() => new CommandRegistrationContext()
                .HandlerReturnsSuccess()
            )
            .Act(context => context.Run("simple"))
            .Assert(result => result.Should().HaveCommandOfType<SimpleCommand>()
                .And.HaveRunHandler()
                .And.NotHaveRunHelp()
                .And.BeSuccessful());
        }

        [Theory]
        [InlineData("--help")]
        [InlineData("-?")]
        [InlineData("-h")]
        public async Task CommandWithHelpShouldRunHelpHandler(string helpCommand)
        {
            await Arrange(() => new CommandRegistrationContext()
                    .HandlerReturnsSuccess()
                ).Act(context => context.Run($"simple {helpCommand}"))
                .Assert(result => result.Should().HaveCommandOfType<SimpleCommand>()
                    .And.HaveRunHelp()
                    .And.NotHaveRunHandler()
                    .And.BeSuccessful());
        }
        
        [Fact]
        public async Task UnmatchedCommandsShouldReturnAnError()
        {
            await Arrange(() => new CommandRegistrationContext()
                    .HandlerReturnsSuccess()
                ).Act(context => context.Run("unknown"))
                .Assert(result => result.Should().NotHaveACommand()
                    .And.NotHaveRunHandler()
                    .And.NotHaveRunHelp()
                    .And.HaveFailed()
                    .And.HaveErrorCode("command_not_found"));
        }
        
        [Fact]
        public async Task PassNothing()
        {
            await Arrange(() => new CommandRegistrationContext().HandlerReturnsSuccess())
                .Act(context => context.Run(""))
                .Assert(result => result.Should().NotHaveACommand()
                    .And.NotHaveRunHandler()
                    .And.NotHaveRunHelp()
                    .And.HaveFailed()
                    .And.HaveErrorCode("invalid_args"));
        }
    }
}
