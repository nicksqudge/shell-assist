namespace DotnetCQRS.CLIParser.Tests.Helpers;

public abstract class ArrangeActAssert
{
    public Arranged<T> Arrange<T>(Func<T> arrangeAction)
    {
        return new Arranged<T>(arrangeAction.Invoke());
    }
}

public class Arranged<T>
{
    private readonly T _input;

    public Arranged(T input)
    {
        _input = input;
    }

    public Acted<TOutput> Act<TOutput>(Func<T, TOutput> actAction)
    {
        return new Acted<TOutput>(actAction.Invoke(_input));
    }

    public ActedAsync<TOutput> Act<TOutput>(Func<T, Task<TOutput>> actAction)
    {
        return new ActedAsync<TOutput>(actAction.Invoke(_input));
    }
}

public class Acted<T>
{
    private readonly T _input;

    public Acted(T input)
    {
        _input = input;
    }

    public void Assert(Action<T> assertAction)
    {
        assertAction.Invoke(_input);
    }
}

public class ActedAsync<T>
{
    private readonly Task<T> _input;

    public ActedAsync(Task<T> input)
    {
        _input = input;
    }

    public async Task Assert(Action<T> assertAction)
    {
        assertAction.Invoke(await _input);
    }
}