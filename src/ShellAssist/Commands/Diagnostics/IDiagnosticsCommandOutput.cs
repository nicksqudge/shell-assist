namespace ShellAssist.Commands.Diagnostics;

public interface IDiagnosticsCommandOutput
{
    void ConfigDirExists();
    void ConfigDirDoesNotExist(bool canBeFixed, string directoryPath);
}