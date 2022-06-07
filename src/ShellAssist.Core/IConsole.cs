namespace ShellAssist.Core;

public interface IConsole
{
    void WriteLine(string text);
    void WriteSuccess(string text);
    void WriteError(string text);
}