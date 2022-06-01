using DotnetCQRS.CLIParser.Tests.CommandRegistration;
using DotnetCQRS.CLIParser.Tests.Helpers;
using FluentAssertions;

namespace DotnetCQRS.CLIParser.Tests.CommandInput;

public class CommandInputTests : ArrangeActAssert
{
    [Theory]
    [InlineData("simple --name acdc", "acdc", default, default)]
    [InlineData("simple --flag", default, true, default)]
    [InlineData("simple 18", default, default, 18)]
    [InlineData("simple 15 --name abc --flag", "abc", true, 15)]
    [InlineData("simple 12 --flag", default, true, 12)]
    [InlineData("simple --name fgh --flag", "fgh", true, default)]
    [InlineData("simple --flag --name ijk", "ijk", true, default)]
    public async Task CombinationCheck(string input, string expectedParameter, bool expectedOption, int expectedArgument)
    {
        await Arrange(() => new CommandInputContext().SetCommandDetail(new CommandDetail<CommandInputContext.InputTestCommand>()
            .AddParameter(t => t.Parameter, "name")
            .AddOption(t => t.Flag, "flag")
            .AddArgument(t => t.Argument, 1)
        ))
        .Act(context => context.Run(input))
        .Assert(result => result.Command.Should()
            .BeEquivalentTo(new CommandInputContext.InputTestCommand()
            {
                Argument = expectedArgument,
                Flag = expectedOption,
                Parameter = expectedParameter
            }));
    }
}