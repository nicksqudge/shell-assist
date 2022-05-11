using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CliFx.Infrastructure;
using NSubstitute;
using ShellAssist.Commands.AddCommand;
using ShellAssist.Tests.TestHelpers;
using ShellAssist.Tests.TestHelpers.Builders;
using Xunit;

namespace ShellAssist.Tests.Commands;

public class AddCommandTests
{
    private readonly AddCommand _command;
    private readonly IConsole _console;
    private readonly IOperatingSystem _os;

    public AddCommandTests()
    {
        _console = new FakeConsole();
        _os = Substitute.For<IOperatingSystem>();
        _command = new AddCommand(_os);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task InvalidCommandName(string input)
    {
        _command.Name = input;

        var result = _command.ExecuteAsync(_console);
        result.Should().HaveFailed();
    }

    [Fact]
    public async Task CreateCommandWithoutConfigDirectory()
    {
        _command.Name = "valid-name";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().DoesNotExist().Build());

        var result = _command.ExecuteAsync(_console);
        result.Should().HaveSucceeded();
        _os.ReceivedWithAnyArgs().CreateDirectory(default);
    }

    [Fact]
    public async Task CreateCommandWithExistingDirectory()
    {
        _command.Name = "valid-name";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());

        var result = _command.ExecuteAsync(_console);
        result.Should().HaveSucceeded();
        _os.DidNotReceiveWithAnyArgs().CreateDirectory(default);
    }

    [Fact]
    public async Task ValidCommandName()
    {
        _command.Name = "simple-command";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());

        var result = _command.ExecuteAsync(_console);
        result.Should().HaveSucceeded();
        _os.Received().CreateFile(
            Arg.Any<string>(),
            Arg.Is<string>(input => input.Contains("simple-command.json")), 
            Arg.Any<string>()
        );
    }

    [Theory]
    [InlineData(" Test Command", "test-command")]
    [InlineData("test command", "test-command")]
    [InlineData("Something", "something")]
    [InlineData("Something!", "something")]
    [InlineData("some.thing", "something")]
    public async Task SimplifyCommandName(string input, string expectedName)
    {
        _command.Name = input;
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());

        var result = _command.ExecuteAsync(_console);
        result.Should().HaveSucceeded();
        _os.Received().CreateFile(
            Arg.Any<string>(),
            Arg.Is<string>(input => input.Contains($"{expectedName}.json")), 
            Arg.Any<string>()
        );
    }

    [Fact]
    public async Task CommandAlreadyExists()
    {
        _command.Name = "simple-command";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());
        _os.DoesFileExist(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(true);

        var result = _command.ExecuteAsync(_console);
        result.Should().HaveFailed();
        _os.DidNotReceive().CreateFile(
            Arg.Any<string>(),
            Arg.Any<string>(), 
            Arg.Any<string>()
        );
    }
}