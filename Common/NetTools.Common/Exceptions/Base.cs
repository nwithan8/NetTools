using System;

namespace NetTools.Common.Exceptions;

public abstract class NetToolsException : Exception
{
    protected NetToolsException(string message) : base(message)
    {
    }
    
    /// <summary>
    ///     Get a formatted error string with expanded details about the EasyPost API error.
    /// </summary>
    /// <returns>A formatted error string.</returns>
    public abstract string PrettyPrint { get; }
}
