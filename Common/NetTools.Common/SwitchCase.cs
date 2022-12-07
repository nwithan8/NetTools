using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetTools.Common;
// Thanks to https://stackoverflow.com/a/34388226/13343799

public class Scenario : ValidationEnum
{
    /// <summary>
    ///     Run associated action when no other cases match. <seealso cref="NoMatch" />
    /// </summary>
    public static Scenario Default => NoMatch;

    /// <summary>
    ///     Run associated action when multiple cases match.
    /// </summary>
    public static Scenario MultipleMatch => new(4, matchCount => (int)matchCount! > 1);

    /// <summary>
    ///     Run associated action when no other cases match. <seealso cref="Default" />
    /// </summary>
    public static Scenario NoMatch => new(2, matchCount => (int)matchCount! == 0);

    /// <summary>
    ///     Run associated action when only one case matches.
    /// </summary>
    public static Scenario SingleMatch => new(3, matchCount => (int)matchCount! == 1);

    private Scenario(int id, Func<object?, bool>? value) : base(id, value)
    {
    }

    /// <summary>
    ///     Run associated action when at least X number of cases match.
    /// </summary>
    /// <param name="count">Minimum number of cases required to match to trigger action.</param>
    /// <returns>Scenario</returns>
    public static Scenario AtLeastXMatch(int count)
    {
        return new Scenario(5, matchCount => (int)matchCount! >= count);
    }

    /// <summary>
    ///     Run associated action when at most X number of cases match.
    /// </summary>
    /// <param name="count">Maximum number of cases required to match to trigger action.</param>
    /// <returns>Scenario</returns>
    public static Scenario AtMostXMatch(int count)
    {
        return new Scenario(6, matchCount => (int)matchCount! <= count);
    }

    /// <summary>
    ///     Run associated action when between X and Y number of cases match.
    /// </summary>
    /// <param name="min">Minimum number of cases required to match to trigger action.</param>
    /// <param name="max">Maximum number of cases required to match to trigger action.</param>
    /// <returns>Scenario</returns>
    public static Scenario BetweenXandXMatch(int min, int max)
    {
        return new Scenario(7, matchCount => (int)matchCount! >= min && (int)matchCount! <= max);
    }

    /// <summary>
    ///     Run associated action when exactly X number of cases match.
    /// </summary>
    /// <param name="count">Exact number of cases required to match to trigger action.</param>
    /// <returns>Scenario</returns>
    public static Scenario ExactlyXMatch(int count)
    {
        return new Scenario(8, matchCount => (int)matchCount! == count);
    }
}

/// <summary>
///     Base interface for all switch-case case types.
/// </summary>
public interface ICase
{
    public Action? Action { get; }

    public object Value { get; }
}

/// <summary>
///     Represents a case in a switch statement with an expression to evaluate and an action to take.
/// </summary>
public class ExpressionCase : ICase
{
    public Action? Action { get; }

    public object Value => Expression.Invoke();

    private Func<object> Expression { get; }

    internal ExpressionCase(Func<object> expression, Action? action)
    {
        Expression = expression;
        Action = action;
    }
}

/// <summary>
///     Represents a case in a switch statement with a static value to match and an action to take.
/// </summary>
public sealed class StaticCase : ExpressionCase
{
    public StaticCase(object value, Action? action) : base(() => value, action)
    {
    }
}

internal sealed class ScenarioCase
{
    public Action? Action { get; }

    private Scenario Scenario { get; }

    internal ScenarioCase(Scenario scenario, Action? action)
    {
        Scenario = scenario;
        Action = action;
    }

    internal bool Validate(object value)
    {
        return Scenario.Validate(value);
    }
}

/// <summary>
///     A custom switch-case implementation that can handle non-constants and custom enums.
/// </summary>
public class SwitchCase : IEnumerable<ICase>
{
    private readonly List<ICase> _cases = new();

    private readonly List<ScenarioCase> _scenarioCases = new();

    /// <summary>
    ///     Add a case where matching a static value triggers an Action
    /// </summary>
    /// <param name="value">Static value to match.</param>
    /// <param name="action">Action to trigger on match.</param>
    public void Add(object value, Action? action)
    {
        _cases.Add(new StaticCase(value, action));
    }

    /// <summary>
    ///     Add a case where matching the result of an function triggers an Action
    /// </summary>
    /// <param name="func">Func whose return value to match.</param>
    /// <param name="action">Action to trigger on match.</param>
    public void Add(Func<object> func, Action? action)
    {
        _cases.Add(new StaticCase(func.Invoke(), action));
    }

    /// <summary>
    ///     Add a case to store Actions in special scenarios. Overrides any previously-set actions for the same scenario.
    /// </summary>
    /// <param name="scenario">CaseEnum to trigger special storage.</param>
    /// <param name="action">Action to trigger on match.</param>
    public void Add(Scenario scenario, Action? action)
    {
        _scenarioCases.Add(new ScenarioCase(scenario, action));
    }

    /// <summary>
    ///     Execute the action of all matching cases. If no matches are found, execute the default case if set.
    /// </summary>
    /// <param name="value">Value to match.</param>
    public void MatchAll(object value)
    {
        var matchingCases = new List<ICase>();

        foreach (var @case in _cases)
            if (@case.Value.Equals(value))
                matchingCases.Add(@case);

        ProcessMatchingCases(matchingCases.ToList());

        // we process the scenarios last
        ProcessScenarioCases(matchingCases.ToList());
    }

    /// <summary>
    ///     Execute the action of all cases that evaluates to false. If no match is found, execute the default case if set.
    /// </summary>
    public void MatchAllFalse()
    {
        MatchAll(false);
    }

    /// <summary>
    ///     Execute the action of all cases that evaluates to true. If no match is found, execute the default case if set.
    /// </summary>
    public void MatchAllTrue()
    {
        MatchAll(true);
    }

    /// <summary>
    ///     Execute the action of the first matching case. If no match is found, execute the default case if set.
    /// </summary>
    /// <param name="value">Value to match.</param>
    public void MatchFirst(object value)
    {
        var matchingCases = new List<ICase>();

        foreach (var @case in _cases)
        {
            if (!@case.Value.Equals(value)) continue;

            matchingCases.Add(@case);
            break;
        }

        ProcessMatchingCases(matchingCases.ToList());

        // we process the scenarios last
        ProcessScenarioCases(matchingCases.ToList());
    }

    /// <summary>
    ///     Execute the action of the first case that evaluates to false. If no match is found, execute the default case if
    ///     set.
    /// </summary>
    public void MatchFirstFalse()
    {
        MatchFirst(false);
    }

    /// <summary>
    ///     Execute the action of the first case that evaluates to true. If no match is found, execute the default case if set.
    /// </summary>
    public void MatchFirstTrue()
    {
        MatchFirst(true);
    }

    IEnumerator<ICase> IEnumerable<ICase>.GetEnumerator()
    {
        return _cases.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _cases.GetEnumerator();
    }

    /// <summary>
    ///     Execute the associated action for each matching case.
    ///     If there are no matching cases, execute the default case if set.
    /// </summary>
    /// <param name="matchingCases">List of matching cases to process.</param>
    private void ProcessMatchingCases(List<ICase> matchingCases)
    {
        foreach (var @case in matchingCases) @case?.Action?.Invoke();
    }

    /// <summary>
    ///     Execute the associated action for each matching scenario case.
    /// </summary>
    /// <param name="matchingCases">List of matching cases to count.</param>
    private void ProcessScenarioCases(ICollection matchingCases)
    {
        var matchingCaseCount = matchingCases.Count;

        // Default scenario will be processed in here as well
        foreach (var @case in _scenarioCases)
            if (@case.Validate(matchingCaseCount))
                @case.Action?.Invoke();
    }
}
