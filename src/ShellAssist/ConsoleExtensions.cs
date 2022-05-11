using CliFx.Infrastructure;

namespace ShellAssist;

public static class ConsoleExtensions
{
    private static string _tickIcon = "\u221A";
    
    public static void ShowSuccess(this IConsole console, string message)
    {
        console.WriteIcon(_tickIcon, message, ConsoleColor.Green);
    }

    public static void ShowFailure(this IConsole console, string message)
    {
        console.WriteIcon("x", message, ConsoleColor.Red);
    }

    public static void WriteIcon(this IConsole console, string icon, string message, ConsoleColor colour)
    {
        console.WriteInColour($"{icon} ", colour);
        console.Output.Write(message);
    }

    public static void WriteInColour(this IConsole console, string message, ConsoleColor colour)
    {
        var original = console.ForegroundColor;
        console.ForegroundColor = colour;
        console.Output.Write(message);
        console.ForegroundColor = original;
    }
    
    public static void NewLine(this IConsole console)
    {
        console.Output.Write("\r\n");
    }
}