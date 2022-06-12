using DotnetCQRS;
using FluentValidation;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public static class ValidationExtensions
{
    public static IRuleBuilder<T, string> MustBeValidCommandName<T>(this IRuleBuilder<T, string> builder,
        ILocalisationHandler localisationHandler)
        where T : class
    {
        builder
            .NotEmpty()
            .WithMessage(localisationHandler.InvalidCommandName());
        
        builder
            .NotNull()
            .WithMessage(localisationHandler.InvalidCommandName());

        return builder;
    }

    public static IRuleBuilder<T, string> MustBeNonExistentCommand<T>(this IRuleBuilder<T, string> builder, IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
        where T : class
    {
        builder
            .MustAsync(async (name, cancellationToken) =>
            {
                var commandFile = operatingSystem
                    .GetConfig()
                    .GetCommandFile(name);

                var result = await operatingSystem.DoesCommandFileExist(commandFile, cancellationToken);
                return result == false;
            })
            .WithMessage((command, name) => localisationHandler.CommandExists(operatingSystem
                .GetConfig()
                .GetCommandFile(name)));
        
        return builder;
    }
    
    public static IRuleBuilder<T, string> MustBeAnExistingCommand<T>(this IRuleBuilder<T, string> builder, IOperatingSystem operatingSystem, ILocalisationHandler localisationHandler)
        where T : class
    {
        builder
            .MustAsync(async (name, cancellationToken) =>
            {
                var commandFile = operatingSystem
                    .GetConfig()
                    .GetCommandFile(name);

                return await operatingSystem.DoesCommandFileExist(commandFile, cancellationToken);
            })
            .WithMessage(localisationHandler.InvalidCommandName());
        
        return builder;
    }
        
    public static async Task<Result?> ValidateAndOutput<T>(this IValidator<T> validator, T item, IConsole console, CancellationToken cancellationToken)
        where T : class
    {
        var result = await validator.ValidateAsync(item, cancellationToken);
        if (result.IsValid)
            return null;

        foreach (var error in result.Errors)
        {
            console.WriteError(error.ErrorMessage);
        }
        
        return Result.Failure("InvalidInput");
    }
}