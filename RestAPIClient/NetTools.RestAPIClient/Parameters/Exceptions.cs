using System.Reflection;
using NetTools.Common.Exceptions;

namespace NetTools.RestAPIClient.Parameters;
#pragma warning disable SA1649
/// <summary>
///     Base class for all validation errors.
/// </summary>
public abstract class ValidationError : NetToolsException
#pragma warning restore SA1649
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ValidationError" /> class.
    /// </summary>
    /// <param name="message">The error message to print to console.</param>
    protected ValidationError(string message)
        : base(message)
    {
    }

    /// <summary>
    ///     Get a formatted error string with expanded details about the error.
    /// </summary>
    /// <returns>A formatted error string.</returns>
    public override string PrettyPrint => Message;
}

/// <summary>
///     Represents an error that occurs due to an invalid parameter.
/// </summary>
public class InvalidParameterError : ValidationError
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidParameterError" /> class.
    /// </summary>
    /// <param name="parameterName">The name of the invalid parameter.</param>
    /// <param name="followUpMessage">Additional message to include in error message.</param>
    internal InvalidParameterError(string parameterName, string? followUpMessage = "")
        : base($"Invalid parameter: '{parameterName}'. {followUpMessage}")
    {
    }
}

/// <summary>
///     Represents an error that occurs due to an invalid parameter pair.
/// </summary>
public class InvalidParameterPairError : ValidationError
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidParameterPairError" /> class.
    /// </summary>
    /// <param name="firstParameterName">The name of the first parameter in the pair.</param>
    /// <param name="secondParameterName">The name of the second parameter in the pair.</param>
    /// <param name="followUpMessage">Additional message to include in error message.</param>
    internal InvalidParameterPairError(string firstParameterName, string secondParameterName,
        string? followUpMessage = "")
        : base(
            $"Invalid parameter pair: '{firstParameterName}' and '{secondParameterName}'. {followUpMessage}")
    {
    }
}

/// <summary>
///     Represents an error that occurs due to a missing parameter.
/// </summary>
public class MissingParameterError : ValidationError
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MissingParameterError" /> class.
    /// </summary>
    /// <param name="parameterName">Name of the missing parameter.</param>
    internal MissingParameterError(string parameterName)
        : base($"Missing required parameter: '{parameterName}'.")
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="MissingParameterError" /> class.
    /// </summary>
    /// <param name="property">The <see cref="PropertyInfo"/> of the missing property.</param>
    internal MissingParameterError(PropertyInfo property)
        : base($"Missing required parameter: '{property.Name}'.")
    {
    }
}