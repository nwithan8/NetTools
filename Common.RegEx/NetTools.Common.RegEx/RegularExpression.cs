namespace NetTools.Common.RegEx;

public class RegularExpressionToken
{
    public static readonly RegularExpressionToken Digit = new RegularExpressionToken("\\d");
    public static readonly RegularExpressionToken NonDigit = new RegularExpressionToken("\\D");
    public static readonly RegularExpressionToken Word = new RegularExpressionToken("\\w");
    public static readonly RegularExpressionToken NonWord = new RegularExpressionToken("\\W");
    public static readonly RegularExpressionToken WhiteSpace = new RegularExpressionToken("\\s");
    public static readonly RegularExpressionToken NonWhiteSpace = new RegularExpressionToken("\\S");
    public static readonly RegularExpressionToken AnyUnicodeSequence = new RegularExpressionToken("\\X");
    public static readonly RegularExpressionToken NewLine = new RegularExpressionToken("\\n");
    public static readonly RegularExpressionToken CarriageReturn = new RegularExpressionToken("\\r");
    public static readonly RegularExpressionToken Tab = new RegularExpressionToken("\\t");
    public static readonly RegularExpressionToken NullCharacter = new RegularExpressionToken("\\0");
    public static readonly RegularExpressionToken AnyCharacter = new RegularExpressionToken(".");
    public static readonly RegularExpressionToken StartOfLine = new RegularExpressionToken("^");
    public static readonly RegularExpressionToken EndOfLine = new RegularExpressionToken("$");
    public static readonly RegularExpressionToken StartOfWord = new RegularExpressionToken("\\b");
    public static readonly RegularExpressionToken EndOfWord = new RegularExpressionToken("\\B");
    public static readonly RegularExpressionToken ZeroOrMore = new RegularExpressionToken("*");
    public static readonly RegularExpressionToken OneOrMore = new RegularExpressionToken("+");
    public static readonly RegularExpressionToken ZeroOrOne = new RegularExpressionToken("?");
    public static readonly RegularExpressionToken ZeroOrMoreLazy = new RegularExpressionToken("*?");
    public static readonly RegularExpressionToken OneOrMoreLazy = new RegularExpressionToken("+?");
    public static readonly RegularExpressionToken ZeroOrOneLazy = new RegularExpressionToken("??");
    public static readonly RegularExpressionToken ZeroOrMorePossessive = new RegularExpressionToken("*+");
    public static readonly RegularExpressionToken OneOrMorePossessive = new RegularExpressionToken("++");
    public static readonly RegularExpressionToken ZeroOrOnePossessive = new RegularExpressionToken("?+");
    public static readonly RegularExpressionToken GroupStart = new RegularExpressionToken("(");
    public static readonly RegularExpressionToken GroupEnd = new RegularExpressionToken(")");
    public static readonly RegularExpressionToken GroupStartNonCapturing = new RegularExpressionToken("(?:");
    public static readonly RegularExpressionToken GroupStartPositiveLookAhead = new RegularExpressionToken("(?=");
    public static readonly RegularExpressionToken GroupStartNegativeLookAhead = new RegularExpressionToken("(?!");
    public static readonly RegularExpressionToken GroupStartPositiveLookBehind = new RegularExpressionToken("(?<=");
    public static readonly RegularExpressionToken GroupStartNegativeLookBehind = new RegularExpressionToken("(?<!");
    public static readonly RegularExpressionToken GroupEndLookAhead = new RegularExpressionToken(")");
    public static readonly RegularExpressionToken GroupEndLookBehind = new RegularExpressionToken(")");
    public static readonly RegularExpressionToken GroupEndNonCapturing = new RegularExpressionToken(")");
    public static readonly RegularExpressionToken GroupEndPositiveLookAhead = new RegularExpressionToken(")");
    public static readonly RegularExpressionToken GroupEndNegativeLookAhead = new RegularExpressionToken(")");
    public static readonly RegularExpressionToken GroupEndPositiveLookBehind = new RegularExpressionToken(")");
    public static readonly RegularExpressionToken GroupEndNegativeLookBehind = new RegularExpressionToken(")");
    public static readonly RegularExpressionToken Alternation = new RegularExpressionToken("|");

    internal string Value { get; }

    protected RegularExpressionToken(string value)
    {
        Value = value;
    }
}

public class OneOf : RegularExpressionToken
{
    public OneOf(IEnumerable<char> elements) : base($"[{string.Join("", elements)}]")
    {
    }
}

public class NotOneOf : RegularExpressionToken
{
    public NotOneOf(IEnumerable<char> elements) : base($"[^{string.Join("", elements)}]")
    {
    }
}

public class InRange : RegularExpressionToken
{
    public InRange(char start, char end, bool caseSensitive = false) : base(caseSensitive ? $"[{start}-{end}]" : $"[{char.ToLower(start)}-{char.ToLower(end)}{char.ToUpper(start)}-{char.ToUpper(end)}]")
    {
    }
}

public class NotInRange : RegularExpressionToken
{
    public NotInRange(char start, char end, bool caseSensitive = false) : base(caseSensitive ? $"[^{start}-{end}]" : $"[^{char.ToLower(start)}-{char.ToLower(end)}{char.ToUpper(start)}-{char.ToUpper(end)}]")
    {
    }
}

public class Either : RegularExpressionToken
{
    public Either(params RegularExpressionToken[] tokens) : base(string.Join(Alternation.Value, tokens.Select(t => t.Value)))
    {
    }

    public Either(IEnumerable<char> characters) : base(string.Join(Alternation.Value, characters.Select(c => c.ToString())))
    {
    }
}

public class ZeroOrMoreOf : RegularExpressionToken
{
    public ZeroOrMoreOf(params RegularExpressionToken[] tokens) : base(string.Join(Alternation.Value, tokens.Select(t => t.Value)) + ZeroOrMore.Value)
    {
    }

    public ZeroOrMoreOf(IEnumerable<char> characters) : base(string.Join(Alternation.Value, characters.Select(c => c.ToString())) + ZeroOrMore.Value)
    {
    }
}

public class OneOrMoreOf : RegularExpressionToken
{
    public OneOrMoreOf(params RegularExpressionToken[] tokens) : base(string.Join(Alternation.Value, tokens.Select(t => t.Value)) + OneOrMore.Value)
    {
    }

    public OneOrMoreOf(IEnumerable<char> characters) : base(string.Join(Alternation.Value, characters.Select(c => c.ToString())) + OneOrMore.Value)
    {
    }
}

public class ZeroOrOneOf : RegularExpressionToken
{
    public ZeroOrOneOf(params RegularExpressionToken[] tokens) : base(string.Join(Alternation.Value, tokens.Select(t => t.Value)) + ZeroOrOne.Value)
    {
    }

    public ZeroOrOneOf(IEnumerable<char> characters) : base(string.Join(Alternation.Value, characters.Select(c => c.ToString())) + ZeroOrOne.Value)
    {
    }
}

public class ExactlyOf : RegularExpressionToken
{
    public ExactlyOf(int count, params RegularExpressionToken[] tokens) : base(string.Join(Alternation.Value, tokens.Select(t => t.Value)) + $"{{{count}}}")
    {
    }

    public ExactlyOf(int count, IEnumerable<char> characters) : base(string.Join(Alternation.Value, characters.Select(c => c.ToString())) + $"{{{count}}}")
    {
    }
}

public class AtLeastOf : RegularExpressionToken
{
    public AtLeastOf(int count, params RegularExpressionToken[] tokens) : base(string.Join(Alternation.Value, tokens.Select(t => t.Value)) + $"{{{count},}}")
    {
    }

    public AtLeastOf(int count, IEnumerable<char> characters) : base(string.Join(Alternation.Value, characters.Select(c => c.ToString())) + $"{{{count},}}")
    {
    }
}

public class BetweenOf : RegularExpressionToken
{
    public BetweenOf(int min, int max, params RegularExpressionToken[] tokens) : base(string.Join(Alternation.Value, tokens.Select(t => t.Value)) + $"{{{min},{max}}}")
    {
    }

    public BetweenOf(int min, int max, IEnumerable<char> characters) : base(string.Join(Alternation.Value, characters.Select(c => c.ToString())) + $"{{{min},{max}}}")
    {
    }
}

public class Raw : RegularExpressionToken
{
    public Raw(string value) : base(value)
    {
    }
}

public class RegularExpression
{
    private RegularExpressionToken[]? Tokens { get; }

    private string RawPattern { get; } = string.Empty;

    public bool ForceStartOfLine { get; init; }

    public bool ForceEndOfLine { get; init; }

    public RegularExpression(string rawPattern)
    {
        RawPattern = rawPattern;
    }

    public RegularExpression(params RegularExpressionToken[] token)
    {
        Tokens = token;
    }

    public string Pattern
    {
        get
        {
            if (Tokens == null)
            {
                return RawPattern;
            }

            var expression = string.Join("", Tokens.Select(t => t.Value));

            if (ForceStartOfLine && !expression.StartsWith(RegularExpressionToken.StartOfLine.Value))
            {
                expression = $"{RegularExpressionToken.StartOfLine.Value}{expression}";
            }

            if (ForceEndOfLine && !expression.EndsWith(RegularExpressionToken.EndOfLine.Value))
            {
                expression = $"{expression}{RegularExpressionToken.EndOfLine.Value}";
            }

            return expression;
        }
    }
}
