using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using ShellAssist.Core.OperatingSystems;
using ShellAssist.Templates;

namespace ShellAssist.Core.API;

internal class AddCommandHandler : ICommandHandler<AddCommand>
{
    private readonly IOperatingSystem _operatingSystem;
    private readonly IConsole _console;
    private readonly ILocalisationHandler _localisation;
    private readonly IValidator<AddCommand> _validator;
    private readonly ITemplateVersionStore _templateVersionStore;

    public AddCommandHandler(
        IOperatingSystem operatingSystem, 
        IConsole console, 
        ILocalisationHandler localisation,
        IValidator<AddCommand> validator, 
        ITemplateVersionStore templateVersionStore)
    {
        _operatingSystem = operatingSystem;
        _console = console;
        _localisation = localisation;
        _validator = validator;
        _templateVersionStore = templateVersionStore;
    }

    public async Task<Result> HandleAsync(AddCommand command, CancellationToken cancellationToken)
    {
        var isValid = await _validator.ValidateAsync(command, cancellationToken);
        if (isValid.IsValid == false)
        {
            _console.WriteError(_localisation.InvalidCommandName());
            return Result.Failure("InvalidCommandName");
        }
        
        var config = await SetupOrGetConfig(cancellationToken);
        var commandFile = config.GetCommandFile(command.Name);
        if (await _operatingSystem.DoesCommandFileExist(commandFile, cancellationToken))
        {
            _console.WriteError(_localisation.CommandExists(commandFile));
            return Result.Failure("CommandFileExists");
        }

        await WriteCommandFile(commandFile, cancellationToken);
        return Result.Success();
    }

    private async Task<ShellConfig> SetupOrGetConfig(CancellationToken cancellationToken)
    {
        var config = _operatingSystem.GetConfig();
        await CreateDirectoryIfItDoesntExist(config.Directory, cancellationToken);
        await CreateDirectoryIfItDoesntExist(config.GetCommandDirectory(), cancellationToken);
        return config;
    }

    private async Task WriteCommandFile(CommandFile commandFile, CancellationToken cancellationToken)
    {
        var latestTemplate = _templateVersionStore.FetchLatest();
        await _operatingSystem.CreateCommandFile(commandFile, latestTemplate, cancellationToken);
        _console.WriteSuccess(_localisation.CommandCreated(commandFile));
        await _operatingSystem.OpenCommandFile(commandFile, cancellationToken);
    }

    private async Task CreateDirectoryIfItDoesntExist(string directory, CancellationToken cancellationToken)
    {
        if (await _operatingSystem.DoesDirectoryExist(directory, cancellationToken) == false)
            await _operatingSystem.CreateDirectory(directory, cancellationToken);
    }
}