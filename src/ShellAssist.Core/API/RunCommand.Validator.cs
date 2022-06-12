using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class RunCommandValidator : AbstractValidator<EditCommand>
{
    public RunCommandValidator(IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
    {
        RuleFor(x => x.Name)
            .MustBeValidCommandName(localisationHandler)
            .MustBeAnExistingCommand(operatingSystem, localisationHandler);
    }
}