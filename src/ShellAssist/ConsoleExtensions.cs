using CliFx.Infrastructure;

namespace ShellAssist;

public static class ConsoleExtensions
{
    private static string _tickIcon = "\u221A";
    
    public static void WriteSuccess(this IConsole console, string message, ConsoleColor colour=ConsoleColor.White)
    {
        console.WriteIcon(_tickIcon, message, ConsoleColor.Green, colour);
    }

    public static void WriteFailure(this IConsole console, string message, ConsoleColor colour=ConsoleColor.White)
    {
        console.WriteIcon("x", message, ConsoleColor.Red, colour);
    }

    public static void WriteIcon(this IConsole console, string icon, string message, ConsoleColor iconColour, ConsoleColor color)
    {
        console.WriteInColour($"{icon} ", iconColour);
        console.WriteInColour(message, color);
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

    public static void WriteListItem(this IConsole console, string item)
    {
        console.Output.WriteLine($" - {item}");
    }
}