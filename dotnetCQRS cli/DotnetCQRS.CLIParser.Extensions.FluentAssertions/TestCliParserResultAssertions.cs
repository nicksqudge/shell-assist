using DotnetCQRS.CLIParser;
using DotnetCQRS.CLIParser.Extensions.FluentAssertions;
using DotnetCQRS.Extensions.FluentAssertions;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace DotnetCQRS.CLIParser.Extensions.FluentAssertions
{
    public class TestCliParserResultAssertions :
        ReferenceTypeAssertions<TestCliParserResult, TestCliParserResultAssertions>
    {
        public TestCliParserResultAssertions(TestCliParserResult subject) : base(subject)
        {
        }

        private AndConstraint<TestCliParserResultAssertions> And()
            => new AndConstraint<TestCliParserResultAssertions>(this);

        protected override string Identifier => nameof(TestCliParserResultAssertions);

        public AndConstraint<TestCliParserResultAssertions> HaveCommandOfType<T>()
            where T : class, ICliCommand
        {
            this.Subject.Command.Should().NotBeNull();
            this.Subject.Command.Should().BeOfType<T>();
            return And();
        }

        public AndConstraint<TestCliParserResultAssertions> NotHaveACommand()
        {
            this.Subject.Command.Should().BeNull();
            return And();
        }

        public AndConstraint<TestCliParserResultAssertions> HaveRunHandler()
        {
            this.Subject.RanHandler.Should().BeTrue();
            return And();
        }
        
        public AndConstraint<TestCliParserResultAssertions> NotHaveRunHandler()
        {
            this.Subject.RanHandler.Should().BeFalse();
            return And();
        }
        
        public AndConstraint<TestCliParserResultAssertions> HaveRunHelp()
        {
            this.Subject.RanHelp.Should().BeTrue();
            return And();
        }
        
        public AndConstraint<TestCliParserResultAssertions> NotHaveRunHelp()
        {
            this.Subject.RanHelp.Should().BeFalse();
            return And();
        }

        public AndConstraint<TestCliParserResultAssertions> BeSuccessful()
        {
            this.Subject.Result.Should().BeSuccess();
            return And();
        }
        
        public AndConstraint<TestCliParserResultAssertions> HaveFailed()
        {
            this.Subject.Result.Should().BeFailure();
            return And();
        }

        public AndConstraint<TestCliParserResultAssertions> HaveErrorCode(string code)
        {
            this.Subject.Result.Should().HaveErrorCode(code);
            return And();
        }
    }
}

public static class AssertionExtensions
{
    public static TestCliParserResultAssertions Should(this TestCliParserResult result)
    {
        return new TestCliParserResultAssertions(result);
    }
}