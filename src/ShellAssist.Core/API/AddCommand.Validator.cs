using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class AddCommandValidator : AbstractValidator<AddCommand>
{
    public AddCommandValidator(IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
    {
        RuleFor(x => x.Name)
            .MustBeValidCommandName(localisationHandler)
            .MustBeNonExistentCommand(operatingSystem, localisationHandler);
    }
}