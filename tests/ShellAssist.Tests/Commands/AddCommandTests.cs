using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotnetCQRS;
using DotnetCQRS.Extensions.FluentAssertions;
using FluentAssertions;
using NSubstitute;
using NSubstitute.Extensions;
using ShellAssist.Core;
using ShellAssist.Core.API;
using ShellAssist.Core.Localisations;
using ShellAssist.Core.OperatingSystems;
using ShellAssist.Templates;
using ShellAssist.Tests.TestHelpers.Builders;
using Xunit;

namespace ShellAssist.Tests.Commands;

public class AddCommandTests
{
    private readonly AddCommand _command = new AddCommand();
    private readonly AddCommandHandler handler;
    private readonly List<string> _consoleOutput = new List<string>();
    private readonly ILocalisationHandler _localisation = new EnglishHandler();
    private readonly List<string> _commandFiles = new List<string>();
    private readonly IOperatingSystem _os = Substitute.For<IOperatingSystem>();

    public AddCommandTests()
    {
        var console = Substitute.For<IConsole>();
        console.WhenForAnyArgs(x => x.WriteLine(default))
            .Do((input) => _consoleOutput.Add(input.ArgAt<string>(0)));
        
        _os.WhenForAnyArgs(x => x.CreateFile(default, default, default, default))
            .Do((input) => _commandFiles.Add(Path.Combine(input.ArgAt<string>(0), input.ArgAt<string>(1))));

        handler = new AddCommandHandler(_os, console, _localisation);
    }

    private Task<Result> Run()
    {
        return handler.HandleAsync(_command, CancellationToken.None);
    }

    private void ShouldContainCommandFile(string name)
    {
        _consoleOutput.Should().Contain(_localisation.CommandCreated(new CommandFile(ShellConfigBuilder.BaseDir, name)));
        _commandFiles.Should().Contain(name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task InvalidCommandName(string input)
    {
        _command.Name = input;

        var result = await Run();

        result.Should().BeFailure();
        _consoleOutput.Should().Contain(_localisation.InvalidCommandName());
        _commandFiles.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateCommandWithoutConfigDirectory()
    {
        _command.Name = "valid-name";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().DoesNotExist().Build());

        var result = await Run();
        result.Should().BeSuccess();
        ShouldContainCommandFile("valid-name.json");
        await _os.ReceivedWithAnyArgs().CreateDirectory(default, default);
    }

    [Fact]
    public async Task CreateCommandWithExistingDirectory()
    {
        _command.Name = "valid-name";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());

        var result = await Run();
        result.Should().BeSuccess();
        ShouldContainCommandFile("valid-name.json");
        await _os.DidNotReceiveWithAnyArgs().CreateDirectory(default, default);
    }

    [Fact]
    public async Task ValidCommandNameAndOpenFile()
    {
        _command.Name = "simple-command";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());

        var result = await Run();
        result.Should().BeSuccess();
        await _os.ReceivedWithAnyArgs().OpenFile(default, default, default);
        ShouldContainCommandFile("simple-command.json");
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

        var result = await Run();
        result.Should().BeSuccess();
        ShouldContainCommandFile($"{expectedName}.json");
    }

    [Fact]
    public async Task CommandAlreadyExists()
    {
        _command.Name = "simple-command";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());
        _os.DoesFileExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(true);

        var result = await Run();
        result.Should().BeFailure();
        _consoleOutput.Should().Contain(_localisation.CommandExists(new CommandFile(ShellConfigBuilder.BaseDir, "simple-command")));
        _commandFiles.Should().NotContain(ShellConfigBuilder.BaseDir + "/simple-command.json");
    }
}