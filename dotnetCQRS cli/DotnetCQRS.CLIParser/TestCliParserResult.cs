using DotnetCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCQRS.CLIParser
{
    public class TestCliParserResult
    {
        public ICliCommand Command { get; set; }

        public bool RanHandler { get; set; }

        public bool RanHelp { get; set; }
        
        public Result Result { get; set; }
    }
}
