namespace ShellAssist.Commands.Diagnostics;

public interface IDiagnosticsCommandOutput
{
    void ConfigExists();
    void ConfigDoesNotExist();
}