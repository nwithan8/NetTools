using System;
using System.Collections.Generic;

namespace NetTools.Common;

public static class ValidationExtensionMethods
{
    public static bool AllExist(this IEnumerable<object?> elements)
    {
        return Validation.AllExist(elements);
    }

    public static bool AllExist(this object?[] elements)
    {
        return Validation.AllExist(elements);
    }

    public static bool AnyDoNotExist(this IEnumerable<object?> elements)
    {
        return Validation.AnyDoNotExist(elements);
    }

    public static bool AnyDoNotExist(this object?[] elements)
    {
        return Validation.AnyDoNotExist(elements);
    }

    public static bool AnyExist(this IEnumerable<object?> elements)
    {
        return Validation.AnyExist(elements);
    }

    public static bool AnyExist(this object?[] elements)
    {
        return Validation.AnyExist(elements);
    }

    public static bool AtLeast(this IEnumerable<object?> elements, int number, Func<object?, bool> check)
    {
        return Validation.AtLeast(number, check, elements);
    }

    public static bool AtLeast(this object?[] elements, int number, Func<object?, bool> check)
    {
        return Validation.AtLeast(number, check, elements);
    }

    public static bool AtLeastOne(this IEnumerable<object?> elements, Func<object?, bool> check)
    {
        return Validation.AtLeastOne(check, elements);
    }

    public static bool AtLeastOne(this object?[] elements, Func<object?, bool> check)
    {
        return Validation.AtLeastOne(check, elements);
    }

    public static bool AtLeastOneDoesNotExist(this IEnumerable<object?> elements)
    {
        return Validation.AtLeastOneDoesNotExist(elements);
    }

    public static bool AtLeastOneDoesNotExist(this object?[] elements)
    {
        return Validation.AtLeastOneDoesNotExist(elements);
    }

    public static bool AtLeastOneExists(this IEnumerable<object?> elements)
    {
        return Validation.AtLeastOneExists(elements);
    }

    public static bool AtLeastOneExists(this object?[] elements)
    {
        return Validation.AtLeastOneExists(elements);
    }

    public static bool AtLeastXDoNotExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.AtLeastXDoNotExist(x, elements);
    }

    public static bool AtLeastXDoNotExist(this object?[] elements, int x)
    {
        return Validation.AtLeastXDoNotExist(x, elements);
    }

    public static bool AtLeastXExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.AtLeastXExist(x, elements);
    }

    public static bool AtLeastXExist(this object?[] elements, int x)
    {
        return Validation.AtLeastXExist(x, elements);
    }

    public static bool AtMost(this IEnumerable<object?> elements, int number, Func<object?, bool> check)
    {
        return Validation.AtMost(number, check, elements);
    }

    public static bool AtMost(this object?[] elements, int number, Func<object?, bool> check)
    {
        return Validation.AtMost(number, check, elements);
    }

    public static bool AtMostOne(this IEnumerable<object?> elements, Func<object?, bool> check)
    {
        return Validation.AtMostOne(check, elements);
    }

    public static bool AtMostOne(this object?[] elements, Func<object?, bool> check)
    {
        return Validation.AtMostOne(check, elements);
    }

    public static bool AtMostOneDoesNotExist(this IEnumerable<object?> elements)
    {
        return Validation.AtMostOneDoesNotExist(elements);
    }

    public static bool AtMostOneDoesNotExist(this object?[] elements)
    {
        return Validation.AtMostOneDoesNotExist(elements);
    }

    public static bool AtMostOneExists(this IEnumerable<object?> elements)
    {
        return Validation.AtMostOneExists(elements);
    }

    public static bool AtMostOneExists(this object?[] elements)
    {
        return Validation.AtMostOneExists(elements);
    }

    public static bool AtMostXDoNotExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.AtMostXDoNotExist(x, elements);
    }

    public static bool AtMostXDoNotExist(this object?[] elements, int x)
    {
        return Validation.AtMostXDoNotExist(x, elements);
    }

    public static bool AtMostXExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.AtMostXExist(x, elements);
    }

    public static bool AtMostXExist(this object?[] elements, int x)
    {
        return Validation.AtMostXExist(x, elements);
    }

    public static bool Exactly(this IEnumerable<object?> elements, int number, Func<object?, bool> check)
    {
        return Validation.Exactly(number, check, elements);
    }

    public static bool Exactly(this object?[] elements, int number, Func<object?, bool> check)
    {
        return Validation.Exactly(number, check, elements);
    }

    public static bool ExactlyOne(this IEnumerable<object?> elements, Func<object?, bool> check)
    {
        return Validation.ExactlyOne(check, elements);
    }

    public static bool ExactlyOne(this object?[] elements, Func<object?, bool> check)
    {
        return Validation.ExactlyOne(check, elements);
    }

    public static bool ExactlyOneDoesNotExist(this IEnumerable<object?> elements)
    {
        return Validation.ExactlyOneDoesNotExist(elements);
    }

    public static bool ExactlyOneDoesNotExist(this object?[] elements)
    {
        return Validation.ExactlyOneDoesNotExist(elements);
    }

    public static bool ExactlyOneExists(this IEnumerable<object?> elements)
    {
        return Validation.ExactlyOneExists(elements);
    }

    public static bool ExactlyOneExists(this object?[] elements)
    {
        return Validation.ExactlyOneExists(elements);
    }

    public static bool ExactlyXDoNotExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.ExactlyXDoNotExist(x, elements);
    }

    public static bool ExactlyXDoNotExist(this object?[] elements, int x)
    {
        return Validation.ExactlyXDoNotExist(x, elements);
    }

    public static bool ExactlyXExist(this IEnumerable<object?> elements, int x)
    {
        return Validation.ExactlyXExist(x, elements);
    }

    public static bool ExactlyXExist(this object?[] elements, int x)
    {
        return Validation.ExactlyXExist(x, elements);
    }

    public static bool NoneExist(this IEnumerable<object?> elements)
    {
        return Validation.NoneExist(elements);
    }

    public static bool NoneExist(this object?[] elements)
    {
        return Validation.NoneExist(elements);
    }
}
