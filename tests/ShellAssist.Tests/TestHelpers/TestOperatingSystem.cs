using System;

namespace ShellAssist.Tests.TestHelpers;

public class TestOperatingSystem : IOperatingSystem
{
    private Action<ShellConfig> getConfigAction;
    
    public TestOperatingSystem SetGetConfig(Action<ShellConfig> inputAction)
    {
        getConfigAction = inputAction;
        return this;
    }
    
    public ShellConfig GetConfig()
    {
        var config = new ShellConfig();

        if (getConfigAction != null)
            getConfigAction.Invoke(config);
        
        return config;
    }
}