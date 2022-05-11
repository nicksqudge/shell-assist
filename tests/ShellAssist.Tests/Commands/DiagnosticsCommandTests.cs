using System.Threading.Tasks;
using CliFx.Infrastructure;
using NSubstitute;
using ShellAssist.Commands.Diagnostics;
using ShellAssist.Tests.TestHelpers;
using Xunit;

namespace ShellAssist.Tests.Commands;

public class DiagnosticsCommandTests
{
    [Fact]
    public async Task HasExistingConfigFolder()
    {
        var console = new FakeConsole();

        var os = new TestOperatingSystem()
            .SetGetConfig(c => c.Exists = true);

        var output = Substitute.For<IDiagnosticsCommandOutput>();

        var command = new DiagnosticsCommand(output, os);

        await command.ExecuteAsync(console);
        output.Received().ConfigDirExists();
    }

    [Fact]
    public async Task DoesNotHaveExistingConfigFolder()
    {
        var console = new FakeConsole();

        var os = new TestOperatingSystem()
            .SetGetConfig(c => c.Exists = false);

        var output = Substitute.For<IDiagnosticsCommandOutput>();

        var command = new DiagnosticsCommand(output, os);

        await command.ExecuteAsync(console);
        output.ReceivedWithAnyArgs().ConfigDirDoesNotExist(default, default);
    }
}