using System.CommandLine.Invocation;
using DotnetCQRS.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ShellAssist;

public static class Extensions
{
    public static async Task RunCommand<T>(this IServiceProvider services, InvocationContext context, Func<InvocationContext, T> getCommand)
        where T : class, ICommand
    {
        var dispatcher = services.GetRequiredService<ICommandDispatcher>();
        var result = await dispatcher.RunAsync(getCommand.Invoke(context), new CancellationTokenSource().Token);
        if (result.IsSuccess)
            context.ExitCode = 0;

        context.ExitCode = 1;
    }
}