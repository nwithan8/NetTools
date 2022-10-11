namespace NetTools;

public static class Text
{
    public static string AllCaps(string text)
    {
        return Validation.Exists(text) ? text.ToUpper() : string.Empty;
    }

    public static string AllLowercase(string text)
    {
        return Validation.Exists(text) ? text.ToLower() : string.Empty;
    }

    public static string TitleCase(string text)
    {
        return Validation.Exists(text) ? System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text) : string.Empty;
    }

    public static string Prefix(string text, string prefix)
    {
        return Validation.Exists(text) ? prefix + text : string.Empty;
    }

    public static string Suffix(string text, string suffix)
    {
        return Validation.Exists(text) ? text + suffix : string.Empty;
    }

    public static string RemovePrefix(string text, string prefix)
    {
        return Validation.Exists(text) ? text.Replace(prefix, string.Empty) : string.Empty;
    }

    public static string RemoveSuffix(string text, string suffix)
    {
        return Validation.Exists(text) ? text.Replace(suffix, string.Empty) : string.Empty;
    }
}
