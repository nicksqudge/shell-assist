using System.CommandLine.Invocation;
using DotnetCQRS.Commands;

namespace ShellAssist.Core.API;

public class DeleteCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
}