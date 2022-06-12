using System.CommandLine.Invocation;
using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class EditCommandHandler : ICommandHandler<EditCommand>
{
    private readonly IValidator<EditCommand> _validator;
    private readonly IConsole _console;
    private readonly IOperatingSystem _operatingSystem;
    private readonly ILocalisationHandler _localisationHandler;

    public EditCommandHandler(IValidator<EditCommand> validator, IConsole console, IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
    {
        _validator = validator;
        _console = console;
        _operatingSystem = operatingSystem;
        _localisationHandler = localisationHandler;
    }

    public async Task<Result> HandleAsync(EditCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAndOutput(command, _console, cancellationToken);
        if (validationResult != null)
            return validationResult;

        var commandFile = _operatingSystem
            .GetConfig()
            .GetCommandFile(command.Name);

        await _operatingSystem.OpenCommandFile(commandFile, cancellationToken);
        _console.WriteSuccess(_localisationHandler.CommandFound(commandFile));
        return Result.Success();
    }
}