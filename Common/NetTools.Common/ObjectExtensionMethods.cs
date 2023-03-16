namespace NetTools.Common;

public static class ObjectExtensionMethods
{
    public static bool IsPropertySet(this object obj, string propertyName) => Objects.IsPropertySet(obj, propertyName);
    
    public static bool IsPrimitive(this object? obj) => Objects.IsPrimitive(obj);
}
