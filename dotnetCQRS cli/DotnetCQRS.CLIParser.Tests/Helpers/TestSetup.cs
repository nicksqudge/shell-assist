using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCQRS.CLIParser.Tests.Helpers
{
    public class TestSetup<T, TIn>
        where T : ITestContext<TIn>, new()
    {
        private T context = new T();

        public void Arrange(Action<T> action)
        {
            action.Invoke(context);
        }

        public async Task<ITestContext<TIn>> Act(TIn input)
        {
            await context.Act(input);
            return context;
        }

        public void Assert(Action<T> action)
        {
            action.Invoke(context);
        }
    }

    public interface ITestContext<T>
    {
        Task Act(T input);
    }
}
