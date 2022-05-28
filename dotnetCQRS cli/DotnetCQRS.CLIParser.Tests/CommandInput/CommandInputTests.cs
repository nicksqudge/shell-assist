using DotnetCQRS.CLIParser.Tests.CommandRegistration;
using DotnetCQRS.CLIParser.Tests.Helpers;

namespace DotnetCQRS.CLIParser.Tests.CommandInput;

public class CommandInputTests : TestSetup<CommandInputContext, string>
{
    [Fact]
    public async Task SetupSimpleArgument()
    {
        
    }

    [Fact]
    public async Task SetupSimpleFlag()
    {
        
    }

    [Fact]
    public async Task SetupSimpleOption()
    {
        
    }

    [Theory]
    [InlineData("simple --name acdc", "acdc", default, default)]
    public async Task CombinationCheck(string input, string expectedBasicArgument, bool expectedFlagValue, int expectedOption)
    {
        
    }
}