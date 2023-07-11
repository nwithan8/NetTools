namespace NetTools.Testing.Xunit;

public abstract partial class Assert
{
    private static void GuardArgumentNotNull(string argName, object argValue)
    {
        if (argValue == null)
            throw new ArgumentNullException(argName);
    }
}
