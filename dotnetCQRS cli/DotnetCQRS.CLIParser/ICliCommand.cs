using DotnetCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCQRS.CLIParser
{
    public interface ICliCommand : ICommand
    {
        CommandDetail GetDetail();
    }
}
