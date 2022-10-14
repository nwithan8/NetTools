using System.Reflection;

namespace NetTools.Attributes;

public abstract class CustomAttribute : Attribute, ICustomAttribute
{
    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(Type @type)
    {
        var matchingProperties = new List<PropertyInfo>();

        var properties = @type.GetProperties();

        foreach (var property in properties)
        {
            var attributes = property.GetCustomAttributes(typeof(T), true);

            if (attributes.Any())
            {
                matchingProperties.Add(property);
            }
        }

        return matchingProperties;
    }

    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(object obj) where T : Attribute
    {
        return GetPropertiesWithAttribute<T>(obj.GetType());
    }

    public static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(Type @type)
    {
        var matchingMethods = new List<MethodInfo>();

        var methods = @type.GetMethods();

        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(T), true);

            if (attributes.Any())
            {
                matchingMethods.Add(method);
            }
        }

        return matchingMethods;
    }

    public static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(object obj) where T : Attribute
    {
        return GetMethodsWithAttribute<T>(obj.GetType());
    }
}

internal interface ICustomAttribute
{
}
