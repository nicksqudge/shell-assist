using Newtonsoft.Json;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.ShellCommands.Versions;

public class Version1ShellCommandHandler : IShellCommandHandler<Version1ShellCommandTemplate>
{
    private readonly IConsole _console;
    private readonly ILocalisationHandler _localisationHandler;
    private readonly IOperatingSystem _operatingSystem;

    public Version1ShellCommandHandler(
        IConsole console,
        ILocalisationHandler localisationHandler,
        IOperatingSystem operatingSystem)
    {
        _console = console;
        _localisationHandler = localisationHandler;
        _operatingSystem = operatingSystem;
    }

    public async Task<int> Execute(CommandFile commandFile, CancellationToken cancellationToken)
    {
        try
        {
            string fileContents = await _operatingSystem.ReadCommandFile(commandFile, cancellationToken);
            var commandData = JsonConvert.DeserializeObject<CommandFileJson<Version1ShellCommandTemplate>>(fileContents);

            ValidateCommandData(commandData);
            _console.WriteLine(_localisationHandler.CommandFound(commandFile, commandData.Version));

            foreach (var command in commandData.Command.Commands)
            {
                _console.WriteInfo(_localisationHandler.ExecutingCommand(command.Start, command.Args));
                _console.NewLine();

                await _operatingSystem.ExecutingCommand(
                    command.Start, command.Args,
                    (line) => _console.WriteLine(line),
                    (error) => _console.WriteError(error)
                );
                _console.NewLine();
            }

            return 0;
        }
        catch (Exception e)
        {
            _console.WriteError(e.GetBaseException().Message);
            return 1;
        }
    }

    private void ValidateCommandData(CommandFileJson<Version1ShellCommandTemplate> commandData)
    {
        if (commandData == null)
            throw new Exception(_localisationHandler.InvalidCommandFile());

        if (commandData.Command == null)
            throw new Exception(_localisationHandler.InvalidCommandFile());

        if (commandData.Command.Commands == null)
            throw new Exception(_localisationHandler.InvalidCommandFile());

        if (commandData.Command.Commands.Any() == false)
            throw new Exception(_localisationHandler.InvalidCommandFile());
    }
}