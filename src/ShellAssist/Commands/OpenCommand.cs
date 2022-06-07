using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Commands;
//
// [Command("open", Description = "Open a command")]
// public class OpenCommand : BaseCommand
// {
//     private readonly IOperatingSystem _os;
//
//     public OpenCommand(IOperatingSystem os)
//     {
//         _os = os;
//     }
//
//     [CommandParameter(0, Description = "The name of the command to open")]
//     public string Name { get; set; } = string.Empty;
//
//     protected override ValueTask ExecuteCommandAsync(IConsole console)
//     {
//         throw new NotImplementedException();
//     }
// }