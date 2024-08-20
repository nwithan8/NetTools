using NetTools.Common.Attributes;

namespace NetTools.RestAPIClient.Documentation
{
    /// <summary>
    ///     Custom <see cref="Attribute"/> used to label CRUD operations in this SDK.
    /// </summary>
    internal static class CrudOperationAttribute
    {
        /// <summary>
        ///     A <see cref="CustomAttribute"/> used to label "create" operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Create : CustomAttribute
        {
        }

        /// <summary>
        ///     A <see cref="CustomAttribute"/> used to label "read" operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Read : CustomAttribute
        {
        }

        /// <summary>
        ///     A <see cref="CustomAttribute"/> used to label "update" operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Update : CustomAttribute
        {
        }

        /// <summary>
        ///     A <see cref="CustomAttribute"/> used to label "delete" operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Delete : CustomAttribute
        {
        }
    }
}
