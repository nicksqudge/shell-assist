using DotnetCQRS.Commands;

namespace DotnetCQRS.CLIParser.Tests
{
    internal class SimpleCommand : ICliCommand
    {
        public string BasicArgument { get; set; }
        public bool AFlag { get; set; }
        public int SimpleOption { get; set; }
        
        public CommandDetail GetDetail()
        {
            throw new NotImplementedException();
        }
    }
}
