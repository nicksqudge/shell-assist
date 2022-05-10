using CliFx;
using Microsoft.Extensions.DependencyInjection;
using ShellAssist;
using ShellAssist.Commands;
using ShellAssist.Commands.Diagnostics;

var services = new ServiceCollection()
    .AddTransient<ConsoleHelper>()
    .AddTransient<IOperatingSystem, Windows>()
    .AddTransient<IDiagnosticsCommandOutput, DiagnosticsCommandOutput>()
    .BuildServiceProvider();

await new CliApplicationBuilder()
    .AddCommandsFromThisAssembly()
    .UseTypeActivator(services.GetRequiredService)
    .Build()
    .RunAsync();