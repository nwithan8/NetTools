namespace NetTools.RestAPIClient;

/// <summary>
///     Base class for all REST API exceptions.
/// </summary>
public class ApiException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiException" /> class.
    /// </summary>
    /// <param name="message">The error message to print to console.</param>
    public ApiException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Get a formatted error string with expanded details about the REST API error.
    ///     Override this property in derived classes to provide more detailed error information.
    /// </summary>
    /// <returns>A formatted error string.</returns>
    public virtual string PrettyPrint { get; } = string.Empty;
}