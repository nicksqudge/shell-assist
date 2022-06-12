using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator(IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
    {
        RuleFor(x => x.Name)
            .MustBeValidCommandName(localisationHandler)
            .MustBeAnExistingCommand(operatingSystem, localisationHandler);
    }
}