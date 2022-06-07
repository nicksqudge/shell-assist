using CliFx;
using CliFx.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ShellAssist;
using ShellAssist.Commands;
using ShellAssist.Commands.Diagnostics;
using ShellAssist.OperatingSystems;
using ShellAssist.Templates;

var services = new ServiceCollection()
    .AddTransient<DiagnosticsCommand>()
    .AddTransient<AddCommand>()
    .AddTransient<ListCommand>()
    .AddTransient<IOperatingSystem, Windows>()
    .AddTransient<IDiagnosticsCommandOutput, DiagnosticsCommandOutput>()
    .AddTransient<IConsole, SystemConsole>()
    .AddTransient<ITemplateVersionStore, TemplateVersionStore>()
    .BuildServiceProvider();

return await new CliApplicationBuilder()
    .AddCommandsFromThisAssembly()
    .UseTypeActivator(services.GetRequiredService)
    .Build()
    .RunAsync();