using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetTools.Common;

/// <summary>
///     A Java-like enum implementation for C#.
/// </summary>
public class Enum : IComparable
{
    /// <summary>
    ///     The ID of the enum. Each enum should have a unique ID.
    /// </summary>
    internal int Id { get; }

    protected Enum(int id)
    {
        Id = id;
    }

    /// <summary>
    ///     Compares the current enum to another enum.
    /// </summary>
    /// <param name="other">Another enum to compare against.</param>
    /// <returns>A signed number indicating the relative values of this instance and other.</returns>
    public int CompareTo(object? other)
    {
        return Id.CompareTo(((Enum)other!).Id);
    }

    public override bool Equals(object? obj)
    {
        try
        {
            if (GetType() != obj!.GetType())
                // types are not the same
                return false;

            var objEnum = (Enum)obj;
            return objEnum == this;
        }
        catch (Exception)
        {
            // casting likely failed
            return false;
        }
    }

    public override int GetHashCode()
    {
        {
            return new Dictionary<string, int> { { GetType().ToString(), Id } }.GetHashCode();
        }
    }

    public override string? ToString()
    {
        return Id.ToString();
    }

    private bool Equals(Enum? other)
    {
        return Id == other?.Id;
    }

    public static IEnumerable<T> All<T>() where T : Enum
    {
        return typeof(T).GetFields(BindingFlags.Public |
                                   BindingFlags.Static |
                                   BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();
    }

    public static T? FromId<T>(int id) where T : Enum
    {
        return All<T>().FirstOrDefault(e => e.Id == id);
    }
    
    /// <summary>
    ///     Get the <see cref="Enum"/> associated with the given ID.
    /// </summary>
    /// <param name="value">ID to determine <see cref="Enum"/> from.</param>
    /// <typeparam name="T">Type of <see cref="Enum"/> to return.</typeparam>
    /// <returns>A T-type enum corresponding to the provided value, or null.</returns>
    public static T? FromValue<T>(object? value) where T : Enum
    {
        try
        {
            var @int = Convert.ToInt32(value, CultureInfo.InvariantCulture);
            return FromId<T>(@int);
        }
        catch (Exception)
        {
            // object is not an int
            return null;
        }
    }

    public static bool operator ==(Enum? one, Enum? two)
    {
        if (one is null && two is null) return true;

        if (one is null || two is null) return false;

        return one.Equals(two);
    }

    public static bool operator !=(Enum? one, Enum? two)
    {
        return !(one == two);
    }
    
    public static bool operator <(Enum? one, Enum? two)
    {
        return one!.Id < two!.Id;
    }
    
    public static bool operator >(Enum? one, Enum? two)
    {
        return one!.Id > two!.Id;
    }
    
    public static bool operator <=(Enum? one, Enum? two)
    {
        return one!.Id <= two!.Id;
    }
    
    public static bool operator >=(Enum? one, Enum? two)
    {
        return one!.Id >= two!.Id;
    }
}

/// <summary>
///     An enum that stores a value internally.
/// </summary>
public abstract class ValueEnum : Enum
{
    /// <summary>
    ///     The value of the enum.
    /// </summary>
    public object? Value { get; }

    /// <summary>
    ///     Constructor for the enum.
    /// </summary>
    /// <param name="id">Unique ID for the enum.</param>
    /// <param name="value">Value to store inside the enum.</param>
    protected ValueEnum(int id, object? value) : base(id)
    {
        Value = value;
    }

    public override string? ToString()
    {
        return Value?.ToString();
    }

    public new static T? FromValue<T>(object? value) where T : ValueEnum
    {
        var all = All<T>();
        foreach (var item in all)
        {
            var itemValue = item.Value;

            if (itemValue == null && value == null) return item;

            if (itemValue == null || value == null) continue;

            if (itemValue.Equals(value)) return item;
        }

        return null;
    }
}

/// <summary>
///     An enum that stores multiple values internally.
/// </summary>
public abstract class MultiValueEnum : Enum
{
    /// <summary>
    ///     The values of the enum.
    /// </summary>
    public object?[] Values { get; }
    
    /// <summary>
    ///     The index of the value to use when de/serializing.
    /// </summary>
    public abstract int? SerializerValueIndex { get; }

    /// <summary>
    ///     Constructor for the enum.
    /// </summary>
    /// <param name="id">Unique ID for the enum.</param>
    /// <param name="values">Values to store inside the enum.</param>
    protected MultiValueEnum(int id, params object?[] values) : base(id)
    {
        Values = values;
    }

    public override string? ToString()
    {
        return string.Join(", ", Values);
    }

    public new static T? FromValue<T>(object? value) where T : MultiValueEnum
    {
        return All<T>().FirstOrDefault(item => item.Values.Contains(value));
    }
    
    public static T? FromValueAtIndex<T>(object? value, int index) where T : MultiValueEnum
    {
        if (value == null) return null; // we can't compare nulls, so we can't find the enum
        
        foreach (var @enum in All<T>())
        {
            if (@enum.Values.Length <= index) continue; // skip if index is out of range (avoid throwing exception)
            
            var itemValue = @enum.Values[index];

            if (itemValue == null) continue; // we can't compare nulls, so we can't find the enum
            
            if (itemValue.Equals(value)) return @enum;
        }
        
        return null;
    }
    
    public static T? FromValues<T>(params object?[] values) where T : MultiValueEnum
    {
        var allEnums = All<T>();
        foreach (var item in allEnums)
        {
            if (item.Values.Length != values.Length) continue;

            var found = true;
            for (var i = 0; i < item.Values.Length; i++)
            {
                var itemValue = item.Values[i];
                var value = values[i];

                if (itemValue == null && value == null) continue;

                if (itemValue == null || value == null)
                {
                    found = false;
                    break;
                }

                if (itemValue.Equals(value)) continue;

                found = false;
                break;
            }

            if (found) return item;
        }

        return null;
    }

    public static T? FromValues<T>(IEnumerable<object?> values) where T : MultiValueEnum
    {
        return FromValues<T>(values.ToArray());
    }

    public static T? FromValuesOrder<T>(params object?[] values) where T : MultiValueEnum
    {
        // Strict equality (order of values matters)
        var matchingItem = All<T>().FirstOrDefault(item => item.Values.SequenceEqual(values));
        return matchingItem;
    }

    public static T? FromValuesOrder<T>(IEnumerable<object?> values) where T : MultiValueEnum
    {
        // Strict equality (order of values matters)
        return FromValuesOrder<T>(values.ToArray());
    }
    
    public static int? GetSerializerValueIndex<T>() where T : MultiValueEnum
    {
        var all = All<T>();
        var value = all.FirstOrDefault(item => item.SerializerValueIndex != null);
        return value?.SerializerValueIndex;
    }
}

/// <summary>
///     An enum that stores a validation function internally.
/// </summary>
public abstract class ValidationEnum : Enum
{
    /// <summary>
    ///     The function of the enum.
    ///     Function accepts a single nullable object and returns a boolean.
    /// </summary>
    public Func<object?, bool>? Func { get; }

    /// <summary>
    ///     Constructor for the enum.
    /// </summary>
    /// <param name="id">Unique ID for the enum.</param>
    /// <param name="value">Function to store inside the enum.</param>
    protected ValidationEnum(int id, Func<object?, bool>? value) : base(id)
    {
        Func = value;
    }

    public bool Validate(object? value)
    {
        return Func?.Invoke(value) ?? false;
    }
}

/// <summary>
///     A <see cref="JsonConverter"/> for <see cref="NetTools.Common.Enum"/>s.
/// </summary>
/// <typeparam name="T">The <see cref="NetTools.Common.Enum"/> sub-type to de/serialize.</typeparam>
public class EnumJsonConverter<T> : JsonConverter<NetTools.Common.Enum> where T : NetTools.Common.Enum
{
    public override void WriteJson(JsonWriter writer, NetTools.Common.Enum? value, JsonSerializer serializer)
    {
        var enumValue = value?.Id;
        if (enumValue is null)
        {
            writer.WriteNull();
            return;
        }

        serializer.Serialize(writer, enumValue);
    }

    public override NetTools.Common.Enum? ReadJson(JsonReader reader, Type objectType, NetTools.Common.Enum? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        var jToken = JToken.Load(reader);

        // Extract the value from the JToken
        var enumId = jToken.Value<object>();
        if (enumId is JValue jValue)
            enumId = jValue.Value;

        // Find corresponding enum based on the value
        var target = NetTools.Common.Enum.FromValue<T>(enumId);

        return target ?? null;
    }
}

/// <summary>
///     A <see cref="JsonConverter"/> for <see cref="NetTools.Common.ValueEnum"/>s.
/// </summary>
/// <typeparam name="T">The <see cref="NetTools.Common.ValueEnum"/> sub-type to de/serialize.</typeparam>
public class ValueEnumJsonConverter<T> : JsonConverter<NetTools.Common.ValueEnum> where T : NetTools.Common.ValueEnum
{
    public override void WriteJson(JsonWriter writer, NetTools.Common.ValueEnum? value, JsonSerializer serializer)
    {
        var enumValue = value?.Value;
        if (enumValue is null)
        {
            writer.WriteNull();
            return;
        }

        serializer.Serialize(writer, enumValue);
    }

    public override NetTools.Common.ValueEnum? ReadJson(JsonReader reader, Type objectType, NetTools.Common.ValueEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        var jToken = JToken.Load(reader);

        // Extract the value from the JToken
        var enumValue = jToken.Value<object>();
        if (enumValue is JValue jValue)
            enumValue = jValue.Value;

        // Find corresponding enum based on the value
        var target = NetTools.Common.ValueEnum.FromValue<T>(enumValue);

        return target ?? null;
    }
}

/// <summary>
///     A <see cref="JsonConverter"/> for <see cref="NetTools.Common.MultiValueEnum"/>s.
/// </summary>
/// <typeparam name="T">The <see cref="NetTools.Common.MultiValueEnum"/> sub-type to de/serialize.</typeparam>
public class MultiValueEnumJsonConverter<T> : JsonConverter<NetTools.Common.MultiValueEnum> where T : NetTools.Common.MultiValueEnum
{
    public override void WriteJson(JsonWriter writer, NetTools.Common.MultiValueEnum? value, JsonSerializer serializer)
    {
        // if user has defined a specific value index to serialize, use that
        var enumValue = value?.Values;
        if (enumValue is null)
        {
            writer.WriteNull();
            return;
        }
        
        if (value.SerializerValueIndex == null)
        {
            serializer.Serialize(writer, enumValue);
        }
        else
        {
            var enumValueAtIndex = enumValue[value.SerializerValueIndex.Value]; // will blow up if index is out of range
            serializer.Serialize(writer, enumValueAtIndex);
        }
    }

    public override NetTools.Common.MultiValueEnum? ReadJson(JsonReader reader, Type objectType, NetTools.Common.MultiValueEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        var jToken = JToken.Load(reader);
        
        var serializerIndex = NetTools.Common.MultiValueEnum.GetSerializerValueIndex<T>();
        // if user has not defined a specific value index to serialize, fail
        if (serializerIndex == null)
            throw new JsonSerializationException($"Cannot deserialize {typeof(T).Name} because no serializer value index has been defined.");

        // Extract the value from the JToken
        var enumValue = jToken.Value<object>();
        if (enumValue is JValue jValue)
            enumValue = jValue.Value;

        // Find corresponding enum based on the value
        var target = NetTools.Common.MultiValueEnum.FromValueAtIndex<T>(enumValue, serializerIndex.Value);

        return target ?? null;
    }
}
