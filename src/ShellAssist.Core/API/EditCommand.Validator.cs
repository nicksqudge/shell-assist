using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class EditCommandValidator : AbstractValidator<EditCommand>
{
    public EditCommandValidator(IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
    {
        RuleFor(x => x.Name)
            .MustBeValidCommandName(localisationHandler)
            .MustBeAnExistingCommand(operatingSystem, localisationHandler);
    }
}