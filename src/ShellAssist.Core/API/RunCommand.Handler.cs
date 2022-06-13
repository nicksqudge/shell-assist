using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShellAssist.Core.OperatingSystems;
using ShellAssist.Core.ShellCommands;

namespace ShellAssist.Core.API;

public class RunCommandHandler : ICommandHandler<RunCommand>
{
    private readonly IValidator<RunCommand> _validator;
    private readonly IOperatingSystem _operatingSystem;
    private readonly IShellCommandVersionStore _versionStore;
    private readonly IConsole _console;
    private readonly ILocalisationHandler _localisationHandler;
    private readonly IServiceProvider _services;

    public RunCommandHandler(IValidator<RunCommand> validator, IOperatingSystem operatingSystem, IShellCommandVersionStore versionStore, IConsole console, ILocalisationHandler localisationHandler, IServiceProvider services)
    {
        _validator = validator;
        _operatingSystem = operatingSystem;
        _versionStore = versionStore;
        _console = console;
        _localisationHandler = localisationHandler;
        _services = services;
    }

    public async Task<Result> HandleAsync(RunCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAndOutput(command, _console, cancellationToken);
        if (validationResult != null)
            return validationResult;

        var commandFile = _operatingSystem.GetConfig().GetCommandFile(command.Name);
        var shellLoader = new ShellTemplateLoader(_versionStore.FetchAllVersions());
        string contents = await _operatingSystem.ReadCommandFile(commandFile, cancellationToken);
        var commandTemplateType = shellLoader.LoadTemplate(contents);
        
        if (commandTemplateType == null)
            _console.WriteError(_localisationHandler.InvalidCommandFile());

        var handlerType = typeof(IShellCommandHandler<>).MakeGenericType(new Type[] { commandTemplateType });
        dynamic handler = _services.GetRequiredService(handlerType);
        
        if (handler == null)
            _console.WriteError(_localisationHandler.InvalidCommandTemplateVersion());

        await handler.Execute(commandFile, cancellationToken);
        
        return Result.Success();
    }
}