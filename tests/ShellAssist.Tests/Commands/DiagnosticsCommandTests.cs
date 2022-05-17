using System.Threading.Tasks;
using CliFx.Infrastructure;
using NSubstitute;
using ShellAssist.Commands.Diagnostics;
using ShellAssist.OperatingSystems;
using ShellAssist.Tests.TestHelpers;
using Xunit;

namespace ShellAssist.Tests.Commands;

public class DiagnosticsCommandTests
{
    [Fact]
    public async Task HasExistingConfigFolder()
    {
        var console = new FakeConsole();

        var os = Substitute.For<IOperatingSystem>();
        os.GetConfig().ReturnsForAnyArgs(new ShellConfig()
        {
            Exists = true
        });

        var output = Substitute.For<IDiagnosticsCommandOutput>();

        var command = new DiagnosticsCommand(output, os);

        var result = command.ExecuteAsync(console);
        output.Received().ConfigDirExists();
        result.Should().HaveSucceeded();
    }

    [Fact]
    public async Task DoesNotHaveExistingConfigFolder()
    {
        var console = new FakeConsole();

        var os = Substitute.For<IOperatingSystem>();
        os.GetConfig().ReturnsForAnyArgs(new ShellConfig()
        {
            Exists = false
        });

        var output = Substitute.For<IDiagnosticsCommandOutput>();

        var command = new DiagnosticsCommand(output, os);

        var result = command.ExecuteAsync(console);
        output.ReceivedWithAnyArgs().ConfigDirDoesNotExist(default, default);
        result.Should().HaveFailed();
    }
}