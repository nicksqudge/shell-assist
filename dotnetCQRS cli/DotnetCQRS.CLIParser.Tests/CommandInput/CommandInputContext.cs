using DotnetCQRS.CLIParser.Tests.Helpers;

namespace DotnetCQRS.CLIParser.Tests.CommandInput;

public class CommandInputContext : ITestContext<string>
{
    private CliParser cliParser;
    private TestCliParserResult result;
    
    public async Task Act(string input)
    {
        result = await cliParser.TestParseAsync(input, CancellationToken.None);
    }
}