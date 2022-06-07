using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using ShellAssist.Core;

var services = new ServiceCollection()
    .AddShellAssist()
    .BuildServiceProvider();

var rootCommand = new RootCommand();

return rootCommand.Invoke(args);