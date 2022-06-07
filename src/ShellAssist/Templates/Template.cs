namespace ShellAssist.Templates;

public class Template
{
    public int Version { get; set; }
}
    
public class Template<T> : Template
    where T : ICommandTemplate
{
    public T Command { get; set; } = default!;
}