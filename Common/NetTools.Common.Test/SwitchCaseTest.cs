using System;
using System.Linq.Expressions;
using Xunit;
using NetTools.Common;
using SwitchCase = NetTools.Common.SwitchCase;

namespace NetTools.Tools;

public class SwitchCaseTest
{
    [Fact]
    public void TestSwitchCaseMatchCount()
    {
        var matchCount = 0;

        var @switch = new SwitchCase
        {
            { true, () => matchCount += 1 },
            { false, () => matchCount += 2 },
            { true, () => matchCount += 3 },
            { false, () => matchCount += 4 }
        };

        // If we execute all matches, we should have 2 matches and matchCount should be incremented to 4 (0 original + 1 + 3)
        @switch.MatchAll(true); // find and execute all cases that evaluate to "true"
        Assert.Equal(4, matchCount);

        // If we execute only the first match, we should have 1 match and matchCount should be incremented to 5 (4 original + 1)
        @switch.MatchFirst(true); // find and execute the first case that evaluates to "true"
        Assert.Equal(5, matchCount);
    }

    private static bool ReturnsFalse()
    {
        return false;
    }

    private static bool ReturnsTrue()
    {
        return true;
    }

    [Fact]
    public void TestSwitchCaseCaseTypes()
    {
        var matchCount = 0;

        var @switch = new SwitchCase
        {
            { "string", () => matchCount += 1 },
            { 100, () => matchCount += 1 },
            { 20.000, () => matchCount += 1 },
            { new object(), () => matchCount += 1 },
            { false, () => matchCount += 1 },
            { ReturnsFalse(), () => matchCount += 1 },
            { ReturnsTrue(), () => matchCount += 1 },
            { (Expression<Func<bool>>)(() => false), () => matchCount += 1 },
            { (Expression<Func<bool>>)(() => true), () => matchCount += 1 },
            { Scenario.Default, () => matchCount = -1000 }
        };

        @switch.MatchAll(20.000);
        Assert.Equal(1, matchCount);

        // If there is no match, the default case should be executed
        @switch.MatchAll("no_match");
        Assert.Equal(-1000, matchCount);
    }

    [Fact]
    public void TestSwitchCaseScenarioBasedOnMatchCount()
    {
        var result = string.Empty;

        var @switch = new SwitchCase
        {
            { true, () => result = "first" },
            { true, () => result = "second" },
            { Scenario.MultipleMatch, () => result = "both" }
        };
        @switch.MatchAllTrue();
        
        // normally, it would match the first entry (switch result to "first"), then match the second entry (switch result to "second"),
        // but because we have a scenario for multiple matches, it will switch to "both"
        Assert.Equal("both", result);
    }

    [Fact]
    public void TestSwitchCaseCaseEvaluationOrder()
    {
        var result = string.Empty;

        var @switch = new SwitchCase
        {
            { true, () => result = "first" },
            { true, () => result = "second" },
        };
        @switch.MatchAllTrue();
        
        // it should evaluate and execute the "first" case first, then the "second" case
        Assert.Equal("second", result);
        
        // if we reverse the order, it should execute the "second" case first, then the "first" case
        @switch = new SwitchCase
        {
            { true, () => result = "second" },
            { true, () => result = "first" },
        };
        @switch.MatchAllTrue();
        Assert.Equal("first", result);
    }

    [Fact]
    public void TestSwitchCaseScenarios()
    {
        var @default = false;
        var none = false;
        var single = false;
        var multiple = false;
        var atLeast = false;
        var atMost = false;
        var between = false;
        var exactly2 = false;
        var exactly1 = false;
        
        var @switch = new SwitchCase
        {
            { true, null },
            { true, null },
            { Scenario.Default, () => @default = true },
            { Scenario.NoMatch, () => none = true },
            { Scenario.SingleMatch, () => single = true },
            { Scenario.MultipleMatch, () => multiple = true },
            { Scenario.AtLeastXMatch(1), () => atLeast = true },
            { Scenario.AtMostXMatch(2), () => atMost = true },
            { Scenario.BetweenXandXMatch(1, 2), () => between = true },
            { Scenario.ExactlyXMatch(2), () => exactly2 = true },
            { Scenario.ExactlyXMatch(1), () => exactly1 = true }
        };
        @switch.MatchAllTrue();
        
        Assert.False(@default);
        Assert.False(none);
        Assert.False(single);
        Assert.True(multiple);
        Assert.True(atLeast);
        Assert.True(atMost);
        Assert.True(between);
        Assert.True(exactly2);
        Assert.False(exactly1);
    }
}
