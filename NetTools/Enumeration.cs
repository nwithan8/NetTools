using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetTools
{
    /// <summary>
    ///     A Java-like enum implementation for C#.
    /// </summary>
    public class Enum : IComparable
    {
        /// <summary>
        ///     The ID of the enum. Each enum should have a unique ID.
        /// </summary>
        private int Id { get; }

        protected Enum(int id)
        {
            Id = id;
        }

        /// <summary>
        ///     Compares the current enum to another enum.
        /// </summary>
        /// <param name="other">Another enum to compare against.</param>
        /// <returns>A signed number indicating the relative values of this instance and other.</returns>
        public int CompareTo(object? other) => Id.CompareTo(((Enum)other!).Id);

        public override string? ToString() => Id.ToString();

        public override bool Equals(object? obj)
        {
            try
            {
                if (GetType() != obj!.GetType())
                {
                    // types are not the same
                    return false;
                }

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

        private bool Equals(Enum? other) => Id == other?.Id;

        public static bool operator ==(Enum? one, Enum? two)
        {
            if (one is null && two is null)
            {
                return true;
            }

            if (one is null || two is null)
            {
                return false;
            }

            return one.Equals(two);
        }

        public static bool operator !=(Enum? one, Enum? two)
        {
            return !(one == two);
        }

        public static IEnumerable<T> All<T>() where T : Enum =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        public static T? FromId<T>(int id) where T : Enum
        {
            return All<T>().FirstOrDefault(e => e.Id == id);
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

        public override string? ToString() => Value?.ToString();

        public static T? FromValue<T>(object? value) where T : ValueEnum
        {
            var all = All<T>();
            foreach (var item in all)
            {
                var itemValue = item.Value;

                if (itemValue == null && value == null)
                {
                    return item;
                }

                if (itemValue == null || value == null)
                {
                    continue;
                }

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
        ///     Constructor for the enum.
        /// </summary>
        /// <param name="id">Unique ID for the enum.</param>
        /// <param name="values">Values to store inside the enum.</param>
        protected MultiValueEnum(int id, params object?[] values) : base(id)
        {
            Values = values;
        }

        public override string? ToString() => string.Join(", ", Values);

        public static T? FromValue<T>(object? value) where T : MultiValueEnum
        {
            return All<T>().FirstOrDefault(item => item.Values.Contains(value));
        }

        public static T? FromValues<T>(params object?[] values) where T : MultiValueEnum
        {
            var allEnums = All<T>();
            foreach (var item in allEnums)
            {
                if (item.Values.Length != values.Length)
                {
                    continue;
                }

                var found = true;
                for (var i = 0; i < item.Values.Length; i++)
                {
                    var itemValue = item.Values[i];
                    var value = values[i];

                    if (itemValue == null && value == null)
                    {
                        continue;
                    }

                    if (itemValue == null || value == null)
                    {
                        found = false;
                        break;
                    }

                    if (itemValue.Equals(value)) continue;

                    found = false;
                    break;
                }

                if (found)
                {
                    return item;
                }
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
    }
}
