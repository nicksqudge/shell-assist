using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using ShellAssist.Core.OperatingSystems;
using ShellAssist.Core.ShellCommands;

namespace ShellAssist.Core.API;

public class RunCommandHandler : ICommandHandler<RunCommand>
{
    private readonly IValidator<RunCommand> _validator;
    private readonly IOperatingSystem _operatingSystem;
    private readonly IShellCommandVersionStore _versionStore;
    private readonly IConsole _console;

    public RunCommandHandler(IValidator<RunCommand> validator, IOperatingSystem operatingSystem, IShellCommandVersionStore versionStore, IConsole console)
    {
        _validator = validator;
        _operatingSystem = operatingSystem;
        _versionStore = versionStore;
        _console = console;
    }

    public async Task<Result> HandleAsync(RunCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAndOutput(command, _console, cancellationToken);
        if (validationResult != null)
            return validationResult;

        var commandFile = _operatingSystem.GetConfig().GetCommandFile(command.Name);
        var shellLoader = new ShellTemplateLoader(_versionStore.FetchAllVersions());
        string contents = await _operatingSystem.ReadCommandFile(commandFile, cancellationToken);
        
        

        return Result.Success();
    }
}