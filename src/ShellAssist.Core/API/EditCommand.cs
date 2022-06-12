using DotnetCQRS.Commands;

namespace ShellAssist.Core.API;

public class EditCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
}