using ShellAssist.Core.OperatingSystems;

namespace ShellAssist.Commands.Diagnostics;
//
// [Command("diagnostics", Description = "Run diagnostics on your installation")]
// public class DiagnosticsCommand : ICommand
// {
//     private readonly IOperatingSystem _os;
//     private readonly IDiagnosticsCommandOutput _output;
//
//     public DiagnosticsCommand(IDiagnosticsCommandOutput output, IOperatingSystem os)
//     {
//         _output = output;
//         _os = os;
//     }
//
//     public ValueTask ExecuteAsync(IConsole console)
//     {
//         var failed = false;
//
//         var shellConfig = _os.GetConfig();
//         if (shellConfig.Exists)
//         {
//             _output.ConfigDirExists();
//         }
//         else
//         {
//             _output.ConfigDirDoesNotExist(true, shellConfig.Directory);
//             failed = true;
//         }
//
//         if (failed)
//             throw new CommandException("Diagnostics failed");
//
//         return ValueTask.CompletedTask;
//     }
// }