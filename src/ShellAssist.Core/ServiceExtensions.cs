using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ShellAssist.Core.API;
using FluentValidation;
using ShellAssist.Core.Localisations;
using ShellAssist.Core.OperatingSystems;
using ShellAssist.Templates;

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
            .AddTransient<ITemplateVersionStore, TemplateVersionStore>()
            .AddTransient<ILocalisationHandler, EnglishHandler>();
        
        return services;
    }
}