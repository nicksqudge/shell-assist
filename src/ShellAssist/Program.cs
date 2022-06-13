using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using ShellAssist;
using ShellAssist.Core;
using ShellAssist.Core.API;

var services = new ServiceCollection()
    .AddShellAssist()
    .BuildServiceProvider();

var commandNameArgument = new Argument<string>("name", "The name of the command to create");
var rootCommand = new RootCommand();

var addCommand = new Command("add", "Add a new command");
addCommand.Add(commandNameArgument);
addCommand.AddAlias("a");
addCommand.SetHandler(async context => await services.RunCommand(context, context => new AddCommand
{
    Name = context.ParseResult.GetValueForArgument(commandNameArgument)
}));
rootCommand.Add(addCommand);

var listCommand = new Command("list", "List all commands");
listCommand.AddAlias("ls");
listCommand.SetHandler(async context => await services.RunCommand(context, context => new ListCommand()));
rootCommand.Add(listCommand);

var editCommand = new Command("edit", "Edit command");
editCommand.Add(commandNameArgument);
editCommand.AddAlias("e");
editCommand.SetHandler(async context => await services.RunCommand(context, context => new EditCommand()
{
    Name = context.ParseResult.GetValueForArgument(commandNameArgument)
}));
rootCommand.Add(editCommand);

var deleteCommand = new Command("delete", "Delete a command file");
deleteCommand.Add(commandNameArgument);
deleteCommand.AddAlias("d");
deleteCommand.SetHandler(async context => await services.RunCommand(context, context => new DeleteCommand()
{
    Name = context.ParseResult.GetValueForArgument(commandNameArgument)
}));
rootCommand.Add(deleteCommand);

var runCommand = new Command("run", "Run a command");
runCommand.Add(commandNameArgument);
runCommand.AddAlias("r");
runCommand.SetHandler(async context => await services.RunCommand(context, context => new RunCommand()
{
    Name = context.ParseResult.GetValueForArgument(commandNameArgument)
}));
rootCommand.Add(runCommand);

return rootCommand.Invoke(args);