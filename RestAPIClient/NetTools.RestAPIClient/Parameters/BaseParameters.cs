using System.Collections;
using System.Reflection;
using NetTools.Common;

namespace NetTools.RestAPIClient.Parameters;

/// <summary>
///     Base class for all parameters used in functions.
/// </summary>
public abstract class BaseParameters : IBaseParameters
{
    /*
     * NOTES:
     * Per https://www.informit.com/articles/article.aspx?p=1997935&seqNum=5 and https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values,
     * Any nullable object (non-primitive) will default to `null`
     * Any nullable primitive will default to `null`
     * No need to set a default value for Optional parameters, will be `null` if not set, which is what the internal validator expects
     */

    /// <summary>
    ///     The internal dictionary of parameter key-value pairs.
    /// </summary>
    private Dictionary<string, object?> _parameterDictionary;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseParameters"/> class for a new set of request parameters.
    /// </summary>
    protected BaseParameters() => _parameterDictionary = new Dictionary<string, object?>();

    /// <summary>
    ///     Convert this parameter object to a dictionary for an HTTP request.
    /// </summary>
    /// <returns><see cref="Dictionary{String,TValue}" /> of parameters.</returns>
    public virtual Dictionary<string, object> ToDictionary()
    {
        // NOTE: This method is marked internally on purpose.
        // Bad stuff could happen if we allow end-users to convert a parameter object to a dictionary themselves and try to use it in the normal functions
        // In particular, a lot of the normal functions do additional wrapping of their dictionaries, which would result in invalid JSON schemas being sent to the API

        _parameterDictionary = new Dictionary<string, object?>();

        // Construct the dictionary of all parameters
        var properties = GetType().GetProperties(BindingFlags.Instance |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Public);
        foreach (var property in properties)
        {
            var parameterAttribute =
                NetTools.Common.Attributes.CustomAttribute.GetAttribute<TopLevelRequestParameterAttribute>(property);

            // Ignore any properties that are not annotated with a StandaloneRequestParameterAttribute
            if (parameterAttribute == null)
            {
                continue;
            }

            var value = property.GetValue(this);

            // Check dependent parameters before we finish handling the current parameter
            var dependentParameterAttributes =
                property.GetCustomAttributes<TopLevelRequestParameterDependentsAttribute>();
            foreach (var dependentParameterAttribute in
                     dependentParameterAttributes)
            {
                var dependentParameterResult =
                    dependentParameterAttribute.DependentsAreCompliant(this, value);
                if (!dependentParameterResult.Item1)
                {
                    throw new InvalidParameterPairError(firstParameterName: property.Name,
                        secondParameterName: dependentParameterResult.Item2,
                        followUpMessage: "Please verify the interdependence of these parameters.");
                }
            }

            // If the value is null, check the necessity of the parameter
            if (value == null)
            {
                // If the parameter is required and null, throw an exception
                if (parameterAttribute.Necessity == Necessity.Required)
                {
                    throw new MissingParameterError(property);
                }

                // If the parameter is optional and null, skip it
                continue;
            }

            // Add the non-null value to the dictionary
            Add(parameterAttribute, value);
        }

        // Return the dictionary, removing any null values now that we've verified all required parameters are set
        // Anything still null at this point is an optional parameter that was not set that can be stripped from the request
        return _parameterDictionary.ToStringNonNullableObjectDictionary();
    }

    /// <summary>
    ///     Convert this parameters object to a sub-dictionary, for use in a parent parameter object's dictionary.
    /// </summary>
    /// <param name="parentParameterObjectType">
    ///     The type of the parent parameter object in which this dictionary will be
    ///     embedded.
    /// </param>
    /// <returns><see cref="Dictionary{TKey,TValue}" /> of parameters.</returns>
    public virtual Dictionary<string, object> ToSubDictionary(Type parentParameterObjectType)
    {
        _parameterDictionary = new Dictionary<string, object?>();

        // Construct the dictionary of all parameters
        var properties = GetType().GetProperties(BindingFlags.Instance |
                                                 BindingFlags.NonPublic |
                                                 BindingFlags.Public);
        foreach (var property in properties)
        {
           var parameterAttribute =
                NestedRequestParameterAttribute.GetNestedRequestParameterAttributeForParentType(
                    parentParameterObjectType, property);

            // Ignore any properties that are not annotated with a NestedRequestParameterAttribute or do not have a NestedRequestParameterAttribute for this specific parent type
            if (parameterAttribute == null)
            {
                continue;
            }

            var value = property.GetValue(this);

            // Check dependent parameters before we finish handling the current parameter
            var dependentParameterAttributes =
                property.GetCustomAttributes<NestedRequestParameterDependentsAttribute>();
            foreach (var dependentParameterAttribute in
                     dependentParameterAttributes)
            {
                var dependentParameterResult =
                    dependentParameterAttribute.DependentsAreCompliant(this, value);
                if (!dependentParameterResult.Item1)
                {
                    throw new InvalidParameterPairError(firstParameterName: property.Name,
                        secondParameterName: dependentParameterResult.Item2,
                        followUpMessage: "Please verify the interdependence of these parameters.");
                }
            }

            // If the value is null, check the necessity of the parameter
            if (value == null)
            {
                // If the parameter is required and null, throw an exception
                if (parameterAttribute.Necessity == Necessity.Required)
                {
                    throw new MissingParameterError(property);
                }

                // If the parameter is optional and null, skip it
                continue;
            }

            // Add the non-null value to the dictionary
            Add(parameterAttribute, value);
        }

        // Return the dictionary, removing any null values now that we've verified all required parameters are set
        // Anything still null at this point is an optional parameter that was not set that can be stripped from the request
        return _parameterDictionary.ToStringNonNullableObjectDictionary();
    }

    /// <summary>
    ///     Add a parameter to the dictionary.
    /// </summary>
    /// <param name="requestParameterAttribute"><see cref="RequestParameterAttribute" /> of the parameter to add.</param>
    /// <param name="value">The value of parameter.</param>
    private void Add(RequestParameterAttribute requestParameterAttribute, object? value)
    {
        // Primitive types (i.e. strings, booleans) can be added directly to the dictionary

        if (!NetTools.Common.Objects.IsPrimitive(value))
        {
            value = SerializeObject(value);
        }

        _parameterDictionary.AddOrUpdate(value, requestParameterAttribute.JsonPath);
    }

    private object? SerializeObject(object? obj)
    {
        // If the value is an object type, we know by this point it must inherit the corresponding base-IParameter interface
        // If we've done our job correctly, the only classes that inherit base-IParameter interfaces are base-BaseObject and base-Parameters

        switch (obj)
        {
            // If a given value is an enum, serialize it as a string
            case NetTools.Common.Enum enumObj:
                return enumObj.ToString();
            // If a given value is a base-BaseObject object, serialize it as a dictionary
            case BaseObject baseObject:
                return baseObject.AsDictionary();
            // If the given value is another base-Parameters object, serialize it as a sub-dictionary for the parent dictionary
            // This is because the JSON schema for a sub-object is different than the JSON schema for a top-level object
            // e.g. the schema for an address in the address create API call is different than the schema for an address in the shipment create API call
            case IBaseParameters parameters
                : // TODO: if issues arise with this function, look at the type constraint on BaseParameters here
                return parameters.ToSubDictionary(GetType());
            // If the given value is a list, serialize each element of the list
            case IList list:
            {
                var newList = new List<object?>();
                foreach (var subObj in list)
                {
                    newList.Add(SerializeObject(subObj));
                }

                return newList;
            }
        }

        return obj;
    }
}