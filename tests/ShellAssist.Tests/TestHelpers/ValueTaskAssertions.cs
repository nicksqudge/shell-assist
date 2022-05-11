using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace ShellAssist.Tests.TestHelpers;

public class ValueTaskAssertions : ReferenceTypeAssertions<ValueTask, ValueTaskAssertions>
{
    public ValueTaskAssertions(ValueTask subject) : base(subject)
    {
    }

    protected override string Identifier => nameof(ValueTaskAssertions);

    public void HaveFailed()
    {
        this.Subject.IsCompletedSuccessfully.Should().BeFalse();
    }

    public void HaveSucceeded()
    {
        this.Subject.IsCompletedSuccessfully.Should().BeTrue();
    }
}

public static class ValueTaskExtensions
{
    public static ValueTaskAssertions Should(this ValueTask task)
        => new ValueTaskAssertions(task);
}