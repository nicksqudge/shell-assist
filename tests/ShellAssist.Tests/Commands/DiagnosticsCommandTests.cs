namespace ShellAssist.Tests.Commands;
//
// public class DiagnosticsCommandTests
// {
//     [Fact]
//     public async Task HasExistingConfigFolder()
//     {
//         var console = new FakeConsole();
//
//         var os = Substitute.For<IOperatingSystem>();
//         os.GetConfig().ReturnsForAnyArgs(new ShellConfig()
//         {
//             Exists = true
//         });
//
//         var output = Substitute.For<IDiagnosticsCommandOutput>();
//
//         var command = new DiagnosticsCommand(output, os);
//
//         await command.ExecuteAsync(console);
//         output.Received().ConfigDirExists();
//     }
//
//     [Fact]
//     public async Task DoesNotHaveExistingConfigFolder()
//     {
//         var console = new FakeConsole();
//
//         var os = Substitute.For<IOperatingSystem>();
//         os.GetConfig().ReturnsForAnyArgs(new ShellConfig()
//         {
//             Exists = false
//         });
//
//         var output = Substitute.For<IDiagnosticsCommandOutput>();
//
//         var command = new DiagnosticsCommand(output, os);
//
//         await command.ExecuteAsync(console);
//         output.ReceivedWithAnyArgs().ConfigDirDoesNotExist(default, default);
//     }
// }