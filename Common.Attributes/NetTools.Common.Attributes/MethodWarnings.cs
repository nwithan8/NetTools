using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace NetTools.Common.Attributes;

/// <summary>
///     The base class for all method warnings.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public abstract class MethodWarning : CustomAttribute
{
    private string? Comment { get; }
    protected MethodWarning(string? comment = null)
    {
        Comment = comment;
    }
    
    protected abstract Func<bool> Condition { get; }

    protected abstract string WarningMessage { get; }

    /// <summary>
    ///     Enforces the warning evaluation on the calling method.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public static void Enforce()
    {
        // Get the method that called this method
        var method = (new System.Diagnostics.StackTrace()).GetFrame(1)?.GetMethod();

        var attr = method?.GetCustomAttribute<MethodWarning>();

        if (attr is null) return;
        if (attr.Condition.Invoke()) return;
        
        var warning = attr.WarningMessage;
        if (attr.Comment is not null) warning += $" Comment: {attr.Comment}";
        
        throw new Exception(warning);
    }
}

/// <summary>
///     Mark a method as incomplete with a due date. Will throw an exception if the method is called on or after the due date.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class ToDoBy : MethodWarning
{
    private DateTime Date { get; }

    public ToDoBy(string date, string? comment = null) : base(comment)
    {
        if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            Date = parsedDate;
        }
        else
        {
            throw new ArgumentException("Date must be in the format yyyy-MM-dd");
        }
    }
    protected override Func<bool> Condition => () => DateTime.Now <= Date;
    protected override string WarningMessage => $"This code was supposed to be completed by {Date.ToShortDateString()}.";
}

/// <summary>
///     Mark a method as deprecated with a end date. Will throw an exception if the method is called on or after the end date.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class DeprecationDate : MethodWarning
{
    private DateTime Date { get; }

    public DeprecationDate(string date, string? comment = null) : base(comment)
    {
        if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            Date = parsedDate;
        }
        else
        {
            throw new ArgumentException("Date must be in the format yyyy-MM-dd");
        }
    }
    protected override Func<bool> Condition => () => DateTime.Now <= Date;
    protected override string WarningMessage => $"This code can longer be used as of {Date.ToShortDateString()}.";
}

/// <summary>
///     Mark a method as deprecated with a version. Will throw an exception if the method is called and the application version is newer than or the same as the specified version.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class DeprecationVersion : MethodWarning
{
    private const string Unknown = "Unknown";

    private static string ApplicationVersion
    {
        get
        {
            try
            {
                var assembly = typeof(RuntimeInfo.ApplicationInfo).Assembly;
                var info = FileVersionInfo.GetVersionInfo(assembly.Location);
                return info.FileVersion ?? Unknown;
            }
            catch (Exception)
            {
                return Unknown;
            }
        }
    }
    
    private Version Version { get; }

    private bool IsNewerVersion()
    {
        var applicationVersion = ApplicationVersion;
        if (applicationVersion is Unknown or "")
        {
            throw new Exception("Unable to determine application version.");
        } 
        
        Version.TryParse(applicationVersion, out var currentVersion);
        return currentVersion >= Version;
    }

    public DeprecationVersion(string version, string? comment = null) : base(comment)
    {
        Version.TryParse(version, out var deprecatedVersion);
        Version = deprecatedVersion ?? throw new ArgumentException("Version must be in the format x.x.x.x");
    }

    protected override Func<bool> Condition => IsNewerVersion;
    protected override string WarningMessage => $"This code can longer be used as of {Version}.";
}