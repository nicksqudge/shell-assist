using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class RunCommandValidator : AbstractValidator<RunCommand>
{
    public RunCommandValidator(IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
    {
        RuleFor(x => x.Name)
            .MustBeValidCommandName(localisationHandler)
            .MustBeAnExistingCommand(operatingSystem, localisationHandler);
    }
}