using System.Reflection;

namespace NetTools.Attributes;

public abstract class CustomAttribute : Attribute, ICustomAttribute
{
    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type @type) where T : Attribute
    {
        var properties = @type.GetProperties();

        return (from property in properties let attributes = property.GetCustomAttributes(typeof(T), true) where attributes.Any() select property).ToList();
    }

    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(object obj) where T : Attribute
    {
        return GetPropertiesWithAttribute<T>(obj.GetType());
    }
}

internal interface ICustomAttribute
{
}
