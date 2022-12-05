namespace NetTools.Common.Attributes;

public static class CrudOperations
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal class Create : CustomAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal class Read : CustomAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal class Update : CustomAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal class Delete : CustomAttribute
    {
    }
}
