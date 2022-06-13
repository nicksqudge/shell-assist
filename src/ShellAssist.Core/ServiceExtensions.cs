using System.Runtime.CompilerServices;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShellAssist.Core.Localisations;
using ShellAssist.Core.OperatingSystems;
using ShellAssist.Core.ShellCommands;
using ShellAssist.Core.ShellCommands.Versions;

[assembly: InternalsVisibleTo("ShellAssist.Tests")]

namespace ShellAssist.Core;

public static class ServiceExtensions
{
    public static IServiceCollection AddShellAssist(this IServiceCollection services)
    {
        var assembly = typeof(ServiceExtensions).Assembly;

        services
            .AddDotnetCqrs()
            .AddDefaultCommandDispatcher()
            .AddHandlersFromAssembly(assembly);

        services.AddValidatorsFromAssembly(assembly);

        services
            .AddTransient<IOperatingSystem, Windows>()
            .AddTransient<IShellCommandVersionStore, ShellCommandVersionStore>()
            .AddTransient<ILocalisationHandler, EnglishHandler>()
            .AddTransient<IConsole, Console>()
            .AddTransient<IShellCommandHandler<Version1ShellCommandTemplate>, Version1ShellCommandHandler>();

        return services;
    }
}