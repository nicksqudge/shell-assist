using CliFx;
using CliFx.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ShellAssist;
using ShellAssist.Commands.AddCommand;
using ShellAssist.Commands.Diagnostics;

var services = new ServiceCollection()
    .AddTransient<DiagnosticsCommand>()
    .AddTransient<AddCommand>()
    .AddTransient<IOperatingSystem, Windows>()
    .AddTransient<IDiagnosticsCommandOutput, DiagnosticsCommandOutput>()
    .AddTransient<IConsole, SystemConsole>()
    .BuildServiceProvider();

return await new CliApplicationBuilder()
    .AddCommandsFromThisAssembly()
    .UseTypeActivator(services.GetRequiredService)
    .Build()
    .RunAsync();