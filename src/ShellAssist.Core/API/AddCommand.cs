using DotnetCQRS.Commands;

namespace ShellAssist.Core.API;

public class AddCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
}