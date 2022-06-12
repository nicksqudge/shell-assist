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

    public EditCommandHandler(IValidator<EditCommand> validator, IConsole console, IOperatingSystem operatingSystem)
    {
        _validator = validator;
        _console = console;
        _operatingSystem = operatingSystem;
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
        return Result.Success();
    }
}