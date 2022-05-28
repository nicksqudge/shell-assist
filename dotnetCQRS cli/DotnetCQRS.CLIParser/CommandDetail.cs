namespace DotnetCQRS.CLIParser
{
    public class CommandDetail
    {
        public CommandDetail Describe(string description)
        {
            return this;
        }

        public CommandDetail Option()
        {
            return this;
        }

        public CommandDetail Argument()
        {
            return this;
        }

        public CommandDetail Flag()
        {
            return this;
        }
    }
}