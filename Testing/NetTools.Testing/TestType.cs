namespace NetTools.Testing;

using NetTools.Common.Attributes;

public class TestType
{
    /// <summary>
        ///     Marks a unit test that is testing a method of the same name.
        ///     e.g. public void TestBuy() { ... } should be testing a method called Buy()
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Function : CustomAttribute
        {
        }

        /// <summary>
        ///     Marks a unit test that is testing logic (such as loops or conditionals).
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Logic : CustomAttribute
        {
        }

        /// <summary>
        ///     Marks a unit test that is testing exception handling.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Exception : CustomAttribute
        {
        }

        /// <summary>
        ///     Marks a unit test that is testing properties of an object.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Properties : CustomAttribute
        {
        }

        /// <summary>
        ///     Marks a unit test that is testing "happy path" behavior.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class HappyPath : CustomAttribute
        {
        }

        /// <summary>
        ///     Marks a unit test that is testing edge cases.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class EdgeCase : CustomAttribute
        {
        }

        /// <summary>
        ///     Marks a unit test that is testing the same method as another unit test, but with different parameters.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Parameters : CustomAttribute
        {
        }

        /// <summary>
        ///     Marks a unit test that is doing custom assertions.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        internal sealed class Custom : CustomAttribute
        {
        }
}
