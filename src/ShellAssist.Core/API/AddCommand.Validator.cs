using FluentValidation;

namespace ShellAssist.Core.API;

public class AddCommandValidator : AbstractValidator<AddCommand>
{
    public AddCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}