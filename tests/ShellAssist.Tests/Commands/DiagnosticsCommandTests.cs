using System.Threading.Tasks;
using CliFx.Infrastructure;
using FluentAssertions;
using NSubstitute;
using ShellAssist.Commands;
using Xunit;

namespace ShellAssist.Tests.Commands;

public class DiagnosticsCommandTests
{
    [Fact]
    public void HasExistingConfigFolder()
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
        result.IsCompleted.Should().BeTrue();
        output.Received().ConfigExists();
    }
}