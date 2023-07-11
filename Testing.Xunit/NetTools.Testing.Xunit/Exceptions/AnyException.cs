using Xunit.Sdk;

namespace NetTools.Testing.Xunit.Exceptions
{
    /// <summary>
    /// Exception thrown when an Any assertion has one or more items fail an assertion.
    /// </summary>
    public class AnyException : XunitException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AnyException"/> class.
        /// </summary>
        public AnyException()
            : base("Assert.Any() Failure")
        {
        }
    }
}
