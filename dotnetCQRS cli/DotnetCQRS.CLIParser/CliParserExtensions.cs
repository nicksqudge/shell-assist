using DotnetCQRS.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.CLIParser
{
    public static class CLIParserExtensions
    {
        public static CliParser AddCommand<T>(this CliParser parser, string keyword)
            where T : ICliCommand, new()
        {
            var command = new T();
            return parser.AddCommand(keyword, command);
        }

        public static Task<TestCliParserResult> TestParseAsync(this CliParser parser, string input, CancellationToken cancellationToken)
        {
            string[] args = new string[] { } ;

            if (!string.IsNullOrEmpty(input))
                args = input.Split(" ");

            return parser.TestParseAsync(args, cancellationToken);
        }
    }
}
