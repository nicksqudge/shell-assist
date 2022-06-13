namespace ShellAssist.Core.ShellCommands.Versions;

public class Version1ShellCommandHandler : IShellCommandHandler<Version1ShellCommandTemplate>
{
    private readonly IConsole _console;
    private readonly ILocalisationHandler _localisationHandler;

    public Version1ShellCommandHandler(IConsole console, ILocalisationHandler localisationHandler)
    {
        _console = console;
        _localisationHandler = localisationHandler;
    }

    public Task<int> Execute(Version1ShellCommandTemplate input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}