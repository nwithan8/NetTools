using System;

namespace NetTools.Common.Exceptions;

public class NetToolsException : Exception
{
    internal NetToolsException(string message) : base(message)
    {
    }
}
