namespace NetTools.Common.Attributes;

public static class CrudOperations
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Create : CustomAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Read : CustomAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Update : CustomAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Delete : CustomAttribute
    {
    }
}
