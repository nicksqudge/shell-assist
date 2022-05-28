using DotnetCQRS.CLIParser.Tests.Helpers;

namespace DotnetCQRS.CLIParser.Tests.CommandRegistration
{
    public class CommandRegistrationTests : TestSetup<CommandRegistrationContext, string>
    {
        [Fact]
        public async Task MatchedCommandsShouldRunHandler()
        {
            Arrange(a => a.HandlerReturnsSuccess());

            await Act("simple");

            Assert(a => a
                .ShouldBeSimpleCommand()
                .ShouldHaveRanHandler()
                .ShouldNotHaveRanHelp()
                .ShouldHaveSuccessResult()
            );
        }

        [Theory]
        [InlineData("--help")]
        [InlineData("-?")]
        [InlineData("-h")]
        public async Task CommandWithHelpShouldRunHelpHandler(string helpCommand)
        {
            Arrange(a => a.HandlerReturnsSuccess());

            await Act($"simple {helpCommand}");

            Assert(a => a
                .ShouldBeSimpleCommand()
                .ShouldHaveRanHelp()
                .ShouldNotHaveRanHandler()
                .ShouldHaveSuccessResult()
            );
        }

        [Fact]
        public async Task UnmatchedCommandsShouldReturnAnError()
        {
            Arrange(a => a.HandlerReturnsSuccess());

            await Act("unknown");

            Assert(a => a
                .ShouldHaveNoCommand()
                .ShouldNotHaveRanHandler()
                .ShouldNotHaveRanHelp()
                .ShouldHaveFailedResult()
                .ShouldHaveErrorCode("command_not_found"));
        }

        [Fact]
        public async Task PassNothing()
        {
            Arrange(a => a.HandlerReturnsSuccess());

            await Act("");

            Assert(a => a
                .ShouldHaveNoCommand()
                .ShouldNotHaveRanHandler()
                .ShouldNotHaveRanHelp()
                .ShouldHaveFailedResult()
                .ShouldHaveErrorCode("invalid_args"));
        }
    }
}
