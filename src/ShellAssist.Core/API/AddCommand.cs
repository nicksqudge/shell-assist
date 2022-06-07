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
    public AddCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}

internal class AddCommandHandler : ICommandHandler<AddCommand>
{
    private readonly IOperatingSystem _operatingSystem;
    private readonly IConsole _console;
    private readonly ILocalisationHandler _localisation;
    private readonly IValidator<AddCommand> _validator;

    public AddCommandHandler(IOperatingSystem operatingSystem, IConsole console, ILocalisationHandler localisation,
        IValidator<AddCommand> validator)
    {
        _operatingSystem = operatingSystem;
        _console = console;
        _localisation = localisation;
        _validator = validator;
    }

    public async Task<Result> HandleAsync(AddCommand command, CancellationToken cancellationToken)
    {
        if (_validator.Validate(command).IsValid == false)
        {
            _console.WriteError(_localisation.InvalidCommandName());
            return Result.Failure("InvalidCommandName");
        }
        
        var config = _operatingSystem.GetConfig();
        await CreateDirectoryIfItDoesntExist(config.Directory, cancellationToken);
        await CreateDirectoryIfItDoesntExist(config.GetCommandDirectory(), cancellationToken);

        var commandFile = config.GetCommandFile(command.Name);
        if (await _operatingSystem.DoesCommandFileExist(commandFile, cancellationToken))
        {
            _console.WriteError(_localisation.CommandExists(commandFile));
            return Result.Failure("CommandFileExists");
        }

        _console.WriteSuccess(_localisation.CommandCreated(commandFile));
        return Result.Success();
    }

    private async Task CreateDirectoryIfItDoesntExist(string directory, CancellationToken cancellationToken)
    {
        if (await _operatingSystem.DoesDirectoryExist(directory, cancellationToken) == false)
            await _operatingSystem.CreateDirectory(directory, cancellationToken);
    }
}