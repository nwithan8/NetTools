using Xunit.Sdk;

namespace NetTools.Testing.Xunit.Exceptions
{
    /// <summary>
    /// Exception thrown when an OnlyX assertion has one or more items fail an assertion.
    /// </summary>
    public class OnlyXException : XunitException
    {
        /// <summary>
        /// Creates a new instance of the <see cref="OnlyXException"/> class.
        /// </summary>
        public OnlyXException(string word, int? count = null)
            : base($"Assert.Only{word}() Failure{(count.HasValue ? $": {count.Value} items matched" : string.Empty)}")
        {
        }
    }
}
