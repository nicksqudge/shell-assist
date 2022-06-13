namespace ShellAssist.Core.ShellCommands;

public interface IShellCommandHandler<T>
    where T : class, IShellCommandTemplate
{
    Task<int> Execute(CommandFile commandFile, CancellationToken cancellationToken);
}