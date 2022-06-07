using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class AddCommand : ICommand
{
    public string Name { get; set; }
}

public class AddCommandValidator : AbstractValidator<AddCommand>
{
}

internal class AddCommandHandler : ICommandHandler<AddCommand>
{
    private readonly IOperatingSystem _operatingSystem;
    private readonly IConsole _console;
    private readonly ILocalisationHandler _localisation;

    public AddCommandHandler(IOperatingSystem operatingSystem, IConsole console, ILocalisationHandler localisation)
    {
        _operatingSystem = operatingSystem;
        _console = console;
        _localisation = localisation;
    }

    public async Task<Result> HandleAsync(AddCommand command, CancellationToken cancellationToken)
    {
        var config = _operatingSystem.GetConfig();
        var commandFile = config.GetCommandFile(command.Name);
        if (await _operatingSystem.DoesCommandFileExist(commandFile, cancellationToken))
        {
            _console.WriteError(_localisation.CommandExists(commandFile));
            return Result.Failure("CommandFileExists");
        }

        return Result.Success();
    }
}