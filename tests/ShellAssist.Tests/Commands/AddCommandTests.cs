using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DotnetCQRS;
using DotnetCQRS.Extensions.FluentAssertions;
using FluentAssertions;
using NSubstitute;
using ShellAssist.Core;
using ShellAssist.Core.API;
using ShellAssist.Core.Localisations;
using ShellAssist.Core.OperatingSystems;
using ShellAssist.Core.ShellCommands;
using ShellAssist.Tests.TestHelpers;
using ShellAssist.Tests.TestHelpers.Builders;
using Xunit;

namespace ShellAssist.Tests.Commands;

public class AddCommandTests
{
    private readonly AddCommand _command = new();

    private readonly string _commandDirectory =
        Path.Combine(ShellConfigBuilder.BaseDir, ShellConfig.CommandDirectoryName);

    private readonly List<string> _consoleOutput = new();
    private readonly List<string> _createdDirectories = new();
    private readonly List<string> _createdFiles = new();
    private readonly ILocalisationHandler _localisation = new EnglishHandler();
    private readonly IOperatingSystem _os = Substitute.For<IOperatingSystem>();
    private readonly AddCommandHandler _handler;

    public AddCommandTests()
    {
        var console = new FakeConsole(line => _consoleOutput.Add(line));

        _os.WhenForAnyArgs(x => x.CreateFile(default, default, default, default))
            .Do(input => _createdFiles.Add(Path.Combine(input.ArgAt<string>(0), input.ArgAt<string>(1))));

        _os.WhenForAnyArgs(x => x.CreateDirectory(default, default))
            .Do(input => _createdDirectories.Add(input.ArgAt<string>(0)));

        _handler = new AddCommandHandler(
            _os,
            console,
            _localisation,
            new AddCommandValidator(_os, _localisation),
            new ShellCommandVersionStore()
        );
    }

    private Task<Result> Run()
    {
        return _handler.HandleAsync(_command, CancellationToken.None);
    }

    private void ShouldHaveCreatedCommandFile(string name)
    {
        var fileInfo = new FileInfo(Path.Combine(_commandDirectory, $"{name}.json"));
        var file = new CommandFile(fileInfo);

        _consoleOutput.Should().Contain(_localisation.CommandCreated(file));
        _createdFiles.Should().Contain(file.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task InvalidCommandName(string input)
    {
        _command.Name = input;
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());
        _os.DoesFileExist(default, default, default).ReturnsForAnyArgs(false);

        var result = await Run();

        result.Should().BeFailure();
        _consoleOutput.Should().Contain(_localisation.InvalidCommandName());
        _createdFiles.Should().BeEmpty();
    }

    [Fact]
    public async Task CreateCommandWithoutConfigDirectory()
    {
        _command.Name = "valid-name";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());
        _os.DoesDirectoryExist(Arg.Is<string>(x => x == _commandDirectory), default)
            .ReturnsForAnyArgs(false);

        var result = await Run();

        result.Should().BeSuccess();
        _createdDirectories.Should().Contain(_commandDirectory);
    }

    [Fact]
    public async Task CreateCommandWithExistingDirectory()
    {
        _command.Name = "valid-name";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());
        _os.DoesDirectoryExist(Arg.Is<string>(x => x == _commandDirectory), default)
            .ReturnsForAnyArgs(true);

        var result = await Run();

        result.Should().BeSuccess();
        _createdDirectories.Should().NotContain(_commandDirectory);
    }

    [Fact]
    public async Task ValidCommandNameAndOpenFile()
    {
        _command.Name = "simple-command";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());

        var result = await Run();

        result.Should().BeSuccess();
        ShouldHaveCreatedCommandFile("simple-command");
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
        ShouldHaveCreatedCommandFile(expectedName);
    }

    [Fact]
    public async Task CommandAlreadyExists()
    {
        _command.Name = "simple-command";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());
        _os.DoesFileExist(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(true);

        var result = await Run();

        result.Should().BeFailure();
        _consoleOutput.Should()
            .Contain(_localisation.CommandExists(new CommandFile(_commandDirectory, "simple-command")));
        _createdFiles.Should().NotContain(ShellConfigBuilder.BaseDir + "/simple-command.json");
    }

    [Fact]
    public async Task ShouldOpenCommand()
    {
        _command.Name = "simple-command";
        _os.GetConfig().ReturnsForAnyArgs(ShellConfigBuilder.Typical().Build());

        var result = await Run();
        
        await _os.ReceivedWithAnyArgs().OpenFile(default, default, default);
    }
}