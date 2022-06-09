using System;
using System.Collections.Generic;
using ShellAssist.Core;

namespace ShellAssist.Tests.TestHelpers;

public class FakeConsole : IConsole
{
    private Action<string> writeMethod;
    
    public FakeConsole(Action<string> writeMethod)
    {
        this.writeMethod = writeMethod;
    }
    
    public void WriteLine(string text)
    {
        this.writeMethod.Invoke(text);
    }

    public void WriteSuccess(string text)
    {
        this.writeMethod.Invoke(text);
    }

    public void WriteError(string text)
    {
        this.writeMethod.Invoke(text);
    }

    public void WriteListItem(string text)
    {
        this.writeMethod.Invoke(text);
    }
}