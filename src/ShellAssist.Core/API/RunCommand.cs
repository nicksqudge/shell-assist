using DotnetCQRS.Commands;

namespace ShellAssist.Core.API;

public class RunCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
}