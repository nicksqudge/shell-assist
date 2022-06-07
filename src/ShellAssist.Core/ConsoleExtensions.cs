namespace ShellAssist.Core;

public static class ConsoleExtensions
{
    public static void WriteError(this IConsole console, string message)
    {
        console.WriteLine(message);
    }
}