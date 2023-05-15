using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace NetTools.Common.Attributes;

/// <summary>
///     The base class for all method warnings.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public abstract class MethodWarning : CustomAttribute
{
    private string? Comment { get; }
    protected MethodWarning(string? comment = null)
    {
        Comment = comment;
    }
    
    protected abstract Func<bool> Condition { get; }

    protected abstract string WarningMessage(MethodInfo method);

    private string? CollectPotentialWarningMessage(MethodInfo method)
    {
        if (Condition.Invoke()) return null;
        
        var warning = WarningMessage(method);
        if (Comment is not null) warning += $" Comment: {Comment}";
        
        return warning;
    }

    internal static void Enforce<TMethodWarning>()
    {
        // Go up two levels in the stack to get to the method that called for enforcement
        var method = (new System.Diagnostics.StackTrace()).GetFrame(2)?.GetMethod();
        
        // get the assembly of the method
        var assembly = method?.DeclaringType?.Assembly;
        
        if (assembly is null) return;

        // Get all TMethodWarning-type attributes and their associated methods
        var pairs = assembly
            .GetTypes()
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(typeof(TMethodWarning), false).Length > 0)
            .SelectMany(m => m.GetCustomAttributes(typeof(TMethodWarning), false), (m, a) => new { Method = m, Attribute = a })
            .ToList();

        IEnumerable<string> errorMessages = new List<string>();
        foreach (var pair in pairs)
        {
            var warning = (pair.Attribute as MethodWarning)?.CollectPotentialWarningMessage(pair.Method);
            if (warning is not null)
            {
                errorMessages = errorMessages.Append(warning);
            }
        }

        var errorMessagesList = errorMessages.ToList();

        if (errorMessagesList.Any())
        {
            throw new Exception(string.Join("\n", errorMessagesList));
        }
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
    protected override string WarningMessage(MethodInfo method) => $"{method.Name} was supposed to be completed by {Date.ToShortDateString()}.";
    
    public static void Enforce()
    {
        Enforce<ToDoBy>();
    }
}

/// <summary>
///     Mark a method as deprecated with a date. Will throw an exception if the method exists on or after the date.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class DeprecateByDate : MethodWarning
{
    private DateTime Date { get; }

    public DeprecateByDate(string date, string? comment = null) : base(comment)
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
    protected override string WarningMessage(MethodInfo method) => $"{method.Name} can longer be used as of {Date.ToShortDateString()}.";
    
    public static void Enforce()
    {
        Enforce<DeprecateByDate>();
    }
}

/// <summary>
///     Mark a method as deprecated with a version. Will throw an exception if the method exists on or after the version.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class DeprecateByVersion : MethodWarning
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
    
    private System.Version Version { get; }

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

    public DeprecateByVersion(string version, string? comment = null) : base(comment)
    {
        Version.TryParse(version, out var deprecatedVersion);
        Version = deprecatedVersion ?? throw new ArgumentException("Version must be in the format x.x.x.x");
    }

    protected override Func<bool> Condition => IsNewerVersion;
    protected override string WarningMessage(MethodInfo method) => $"{method.Name} can longer be used as of {Version}.";
    
    public static void Enforce()
    {
        Enforce<DeprecateByVersion>();
    }
}