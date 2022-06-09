namespace ShellAssist.Core;

public interface IConsole
{
    void WriteLine(string text);
    void WriteSuccess(string text);
    void WriteError(string text);
    void WriteListItem(string text);
}

public class Console : IConsole
{
    public void WriteLine(string text)
    {
        System.Console.WriteLine(text);
    }

    public void WriteSuccess(string text)
    {
        WriteLineInColour(text, ConsoleColor.Green);
    }

    public void WriteError(string text)
    {
        WriteLineInColour(text, ConsoleColor.Red);
    }

    public void WriteListItem(string text)
    {
        System.Console.WriteLine($" - {text}");
    }

    private void WriteLineInColour(string text, ConsoleColor colour)
    {
        var original = System.Console.ForegroundColor;
        System.Console.ForegroundColor = colour;
        System.Console.WriteLine(text);
        System.Console.ForegroundColor = original;
    }
}