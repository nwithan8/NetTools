using System;
using System.Linq;

namespace NetTools.Common;

public static class Validation
{
    public static bool All(Func<object?, bool> check, params object?[] elements)
    {
        return elements.All(check);
    }

    public static bool AllExist(params object?[] elements)
    {
        return All(Exists, elements);
    }

    public static bool Any(Func<object?, bool> check, params object?[] elements)
    {
        return elements.Any(check);
    }

    public static bool AnyDoNotExist(params object?[] elements)
    {
        return Any(x => !Exists(x), elements);
    }

    public static bool AnyExist(params object?[] elements)
    {
        return Any(Exists, elements);
    }

    public static bool AtLeast(int number, Func<object?, bool> check, params object?[] elements)
    {
        return elements.Count(check) >= number;
    }

    public static bool AtLeastOne(Func<object?, bool> check, params object?[] elements)
    {
        return AtLeast(1, check, elements);
    }

    public static bool AtLeastOneDoesNotExist(params object?[] elements)
    {
        return AtLeastXDoNotExist(1, elements);
    }

    public static bool AtLeastOneExists(params object?[] elements)
    {
        return AtLeastXExist(1, elements);
    }

    public static bool AtLeastXDoNotExist(int number, params object?[] elements)
    {
        return AtLeast(number, x => !Exists(x), elements);
    }

    public static bool AtLeastXExist(int number, params object?[] elements)
    {
        return AtLeast(number, Exists, elements);
    }

    public static bool AtMost(int number, Func<object?, bool> check, params object?[] elements)
    {
        return elements.Count(check) <= number;
    }

    public static bool AtMostOne(Func<object?, bool> check, params object?[] elements)
    {
        return AtMost(1, check, elements);
    }

    public static bool AtMostOneDoesNotExist(params object?[] elements)
    {
        return AtMostXDoNotExist(1, elements);
    }

    public static bool AtMostOneExists(params object?[] elements)
    {
        return AtMostXExist(1, elements);
    }

    public static bool AtMostXDoNotExist(int number, params object?[] elements)
    {
        return AtMost(number, x => !Exists(x), elements);
    }

    public static bool AtMostXExist(int number, params object?[] elements)
    {
        return AtMost(number, Exists, elements);
    }

    public static bool Exactly(int number, Func<object?, bool> check, params object?[] elements)
    {
        return elements.Count(check) == number;
    }

    public static bool ExactlyOne(Func<object?, bool> check, params object?[] elements)
    {
        return Exactly(1, check, elements);
    }

    public static bool ExactlyOneDoesNotExist(params object?[] elements)
    {
        return ExactlyXDoNotExist(1, elements);
    }

    public static bool ExactlyOneExists(params object?[] elements)
    {
        return ExactlyXExist(1, elements);
    }

    public static bool ExactlyXDoNotExist(int number, params object?[] elements)
    {
        return Exactly(number, x => !Exists(x), elements);
    }

    public static bool ExactlyXExist(int number, params object?[] elements)
    {
        return Exactly(number, Exists, elements);
    }

    public static bool Exists(object? value)
    {
        return value != null;
    }

    public static bool Exists(string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static bool IsCorrectLength(string value, int minLength = 1, int maxLength = 1000000)
    {
        return value.Length >= minLength && value.Length <= maxLength;
    }

    public static bool ItemsMatch(string value1, string value2)
    {
        if (!Exists(value1) || !Exists(value2)) return false;

        return value1.Equals(value2);
    }

    public static bool None(Func<object?, bool> check, params object?[] elements)
    {
        return elements.All(x => !check(x));
    }

    public static bool NoneExist(params object?[] elements)
    {
        return None(Exists, elements);
    }
}
