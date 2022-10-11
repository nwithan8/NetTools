using System.Collections;

namespace NetTools;
// Thanks to https://stackoverflow.com/a/34388226/13343799

public class Scenario : Enum
{
    public static readonly Scenario Default = new(1);

    private Scenario(int id) : base(id)
    {
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
///     Represents a case in a switch statement with a value to match and an action to take.
/// </summary>
public sealed class StaticCase : ExpressionCase
{
    public StaticCase(object value, Action? action) : base(() => value, action)
    {
    }
}

/// <summary>
///     A custom switch-case implementation that can handle non-constants and custom enums.
/// </summary>
public class SwitchCase : IEnumerable<ICase>
{
    private readonly List<ICase> _cases = new();

    private Action? _defaultCaseAction;

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
    ///     Add a case to store Actions in special scenarios. Overrides any previously-set actions for the same scenario.
    /// </summary>
    /// <param name="scenario">CaseEnum to trigger special storage.</param>
    /// <param name="action">Action to trigger on match.</param>
    public void Add(Scenario scenario, Action? action)
    {
        // ironically, we can't use our custom switch-case here, because it would be recursive.
        // instead, back to good ol' if-else statements.
        if (scenario == Scenario.Default)
        {
            // set the default action to trigger if no match(es) found
            _defaultCaseAction = action;
        }
    }

    /// <summary>
    ///     Execute the action of all matching cases. If no matches are found, execute the default case if set.
    /// </summary>
    /// <param name="value">Value to match.</param>
    public void MatchAll(object value)
    {
        var matchingCase = _cases.Where(c => c.Value == value);

        ProcessMatchingCases(matchingCase.ToList());
    }

    /// <summary>
    ///     Execute the action of the first matching case. If no match is found, execute the default case if set.
    /// </summary>
    /// <param name="value">Value to match.</param>
    public void MatchFirst(object value)
    {
        var matchingCases = new List<ICase>();

        var matchingCase = _cases.FirstOrDefault(c => c.Value.Equals(value));
        if (matchingCase != null)
        {
            matchingCases.Add(matchingCase);
        }

        ProcessMatchingCases(matchingCases);
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
        if (matchingCases.Count == 0)
        {
            _defaultCaseAction?.Invoke();
        }

        foreach (var @case in matchingCases)
        {
            @case?.Action?.Invoke();
        }
    }
}
