using System;

namespace DotnetCQRS.CLIParser
{
    public class CommandDetail<T>
        where T : class, ICliCommand<T>
    {
        public CommandDetail<T> AddParameter<TOut>(Func<T, TOut> selector, string name)
        {
            return this;
        }
        
        public CommandDetail<T> AddOption<TOut>(Func<T, TOut> selector, string name)
        {
            return this;
        }

        public CommandDetail<T> AddArgument<TOut>(Func<T, TOut> selector, int position)
        {
            return this;
        }
    }
}