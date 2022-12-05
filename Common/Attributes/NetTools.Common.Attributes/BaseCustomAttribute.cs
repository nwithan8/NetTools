using System.Reflection;

namespace NetTools.Common.Attributes;

public abstract class CustomAttribute : Attribute, ICustomAttribute
{
    /// <summary>
    ///     Get all properties of the specified class that have the specified attribute.
    /// </summary>
    /// <param name="type">Class to find properties in.</param>
    /// <typeparam name="T">Type of attribute to search for.</typeparam>
    /// <returns>List of all properties that have the specified attribute.</returns>
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

    /// <summary>
    ///     Get all properties of the specified object's class that have the specified attribute.
    /// </summary>
    /// <param name="obj">Object whose class to find properties in.</param>
    /// <typeparam name="T">Type of attribute to search for.</typeparam>
    /// <returns>List of all properties that have the specified attribute.</returns>
    public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(object obj) where T : Attribute
    {
        return GetPropertiesWithAttribute<T>(obj.GetType());
    }

    /// <summary>
    ///     Get all methods of the specified class that have the specified attribute.
    /// </summary>
    /// <param name="type">Class to find methods in.</param>
    /// <typeparam name="T">Type of attribute to search for.</typeparam>
    /// <returns>List of all methods that have the specified attribute.</returns>
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

    /// <summary>
    ///     Get all methods of the specified object's class that have the specified attribute.
    /// </summary>
    /// <param name="obj">Object whose class to find methods in.</param>
    /// <typeparam name="T">Type of attribute to search for.</typeparam>
    /// <returns>List of all methods that have the specified attribute.</returns>
    public static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(object obj) where T : Attribute
    {
        return GetMethodsWithAttribute<T>(obj.GetType());
    }

    /// <summary>
    ///     Get the attribute of the specified type for a property.
    /// </summary>
    /// <param name="property">Property to get attribute of.</param>
    /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
    /// <returns>T-type attribute for the property.</returns>
    public static T? GetAttribute<T>(PropertyInfo property) where T : CustomAttribute
    {
        try
        {
            return property.GetCustomAttribute<T>(true);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    ///     Get all attributes of the specified type for a property.
    /// </summary>
    /// <param name="property">Property to get attributes of.</param>
    /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
    /// <returns>All T-type attributes for the property.</returns>
    public static T[]? GetAttributes<T>(PropertyInfo property) where T : CustomAttribute
    {
        var attributes = property.GetCustomAttributes(typeof(T), false);
        if (attributes.Length == 0)
        {
            return null;
        }

        return (T[])attributes;
    }

    /// <summary>
    ///     Get the attribute of the specified type for a method.
    /// </summary>
    /// <param name="method">Method to get attribute of.</param>
    /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
    /// <returns>T-type attribute for the method.</returns>
    public static T? GetAttribute<T>(MethodInfo method) where T : CustomAttribute
    {
        try
        {
            return method.GetCustomAttribute<T>(true);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    ///     Get all attributes of the specified type for a method.
    /// </summary>
    /// <param name="method">Method to get attributes of.</param>
    /// <typeparam name="T">Type of attribute to retrieve.</typeparam>
    /// <returns>All T-type attributes for the method.</returns>
    public static T[]? GetAttributes<T>(MethodInfo method) where T : CustomAttribute
    {
        var attributes = method.GetCustomAttributes(typeof(T), false);
        if (attributes.Length == 0)
        {
            return null;
        }

        return (T[])attributes;
    }

    /// <summary>
    ///     Check if a property has an attribute of the specified type.
    /// </summary>
    /// <param name="property">Property to check.</param>
    /// <typeparam name="T">Type of attribute to search for.</typeparam>
    /// <returns>True if the property has the attribute, False otherwise.</returns>
    public static bool HasCustomAttribute<T>(PropertyInfo property) where T : CustomAttribute
    {
        return property.GetCustomAttribute<T>(true) != null;
    }

    /// <summary>
    ///     Check if a method has an attribute of the specified type.
    /// </summary>
    /// <param name="method">Method to check.</param>
    /// <typeparam name="T">Type of attribute to search for.</typeparam>
    /// <returns>True if the method has the attribute, False otherwise.</returns>
    public static bool HasCustomAttribute<T>(MethodInfo method) where T : CustomAttribute
    {
        return method.GetCustomAttribute<T>(true) != null;
    }
}

internal interface ICustomAttribute
{
}
