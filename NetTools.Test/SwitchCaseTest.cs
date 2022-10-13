﻿using System;
using System.Linq.Expressions;
using Xunit;

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
}
