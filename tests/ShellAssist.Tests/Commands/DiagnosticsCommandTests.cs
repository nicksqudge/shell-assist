using System.Threading.Tasks;
using CliFx.Infrastructure;
using FluentAssertions;
using NSubstitute;
using ShellAssist.Commands;
using ShellAssist.Commands.Diagnostics;
using ShellAssist.Tests.TestHelpers;
using Xunit;

namespace ShellAssist.Tests.Commands;

public class DiagnosticsCommandTests
{
    [Fact]
    public void HasExistingConfigFolder()
    {
        var console = new FakeConsole();

        var os = new TestOperatingSystem()
            .SetGetConfig(c => c.Exists = true);

        var output = Substitute.For<IDiagnosticsCommandOutput>();

        var command = new DiagnosticsCommand(output, os);
        
        var result = command.ExecuteAsync(console);
        result.IsCompleted.Should().BeTrue();
        output.Received().ConfigExists();
    }

    [Fact]
    public void DoesNotHaveExistingConfigFolder()
    {
        var console = new FakeConsole();

        var os = new TestOperatingSystem()
            .SetGetConfig(c => c.Exists = false);

        var output = Substitute.For<IDiagnosticsCommandOutput>();

        var command = new DiagnosticsCommand(output, os);
        
        var result = command.ExecuteAsync(console);
        result.IsCompleted.Should().BeFalse();
        output.Received().ConfigDoesNotExist();
    }
}