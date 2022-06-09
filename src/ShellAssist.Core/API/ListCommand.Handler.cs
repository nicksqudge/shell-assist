using DotnetCQRS;
using DotnetCQRS.Commands;
using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Core.API;

public class ListCommandHandler : ICommandHandler<ListCommand>
{
    private readonly IOperatingSystem _operatingSystem;
    private readonly IConsole _console;

    public ListCommandHandler(IOperatingSystem operatingSystem, IConsole console)
    {
        _operatingSystem = operatingSystem;
        _console = console;
    }

    public async Task<Result> HandleAsync(ListCommand command, CancellationToken cancellationToken)
    {
         var commandDirectory = _operatingSystem.GetConfig().GetCommandDirectory();
         if (string.IsNullOrWhiteSpace(commandDirectory))
             return NoCommandsFoundMessage();

         var files = await _operatingSystem.GetTemplateFilesFromDirectory(commandDirectory, cancellationToken);
         if (!files.Any())
             return NoCommandsFoundMessage();

         foreach (var file in files)
             _console.WriteListItem(file.Name.Replace(".json", ""));
         
         return Result.Success();
    }
    
     private Result NoCommandsFoundMessage()
     {
         _console.WriteLine("No commands found");
         return Result.Failure("NoCommandsFileMessage");
     }
}