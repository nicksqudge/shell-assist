using ShellAssist.Core.ShellCommands;

namespace ShellAssist.Core;

public class CommandFileJson
{
    public int Version { get; set; }
}
    
public class CommandFileJson<T> : CommandFileJson
    where T : IShellCommandTemplate
{
    public T Command { get; set; } = default!;
}