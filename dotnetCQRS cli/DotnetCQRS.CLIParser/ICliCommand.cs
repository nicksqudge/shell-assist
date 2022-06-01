﻿using DotnetCQRS.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCQRS.CLIParser
{
    public interface ICliCommand : ICommand
    {
    }

    public interface ICliCommand<T> : ICliCommand
        where T : class, ICliCommand<T>
    {
        CommandDetail<T> GetDetail();
    }
}
