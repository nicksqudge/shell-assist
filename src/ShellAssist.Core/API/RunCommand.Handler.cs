using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class RunCommandHandler : ICommandHandler<RunCommand>
{
    private readonly IValidator<RunCommand> _validator;
    private readonly IConsole _console;
    private readonly IOperatingSystem _operatingSystem;
    private readonly ILocalisationHandler _localisationHandler;

    public RunCommandHandler(IValidator<RunCommand> validator, IConsole console, IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
    {
        _validator = validator;
        _console = console;
        _operatingSystem = operatingSystem;
        _localisationHandler = localisationHandler;
    }

    public async Task<Result> HandleAsync(RunCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAndOutput(command, _console, cancellationToken);
        if (validationResult != null)
            return validationResult;

        return Result.Success();
    }
}