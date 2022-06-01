using System.Linq;
using DotnetCQRS.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.CLIParser
{
    public static class CLIParserExtensions
    {
        public static CliParser AddCommand<T>(this CliParser parser, string keyword)
            where T : class, ICliCommand<T>, new()
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

        public static string[] RemoveCommandKeyword(this string[] args)
            => args.Skip(1).ToArray();
    }
}
