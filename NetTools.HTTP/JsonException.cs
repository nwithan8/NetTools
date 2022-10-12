namespace NetTools
{
    public class JsonException : Newtonsoft.Json.JsonSerializationException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonException" /> class.
        /// </summary>
        /// <param name="message">Error message.</param>
        internal JsonException(string message) : base(message)
        {
        }
    }

    public class JsonDeserializationException : JsonException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonDeserializationException" /> class.
        /// </summary>
        /// <param name="toType">Type of object attempted creating from JSON.</param>
        internal JsonDeserializationException(Type toType) : base($"Error deserializing JSON into object of type {toType.FullName}.")
        {
        }
    }

    public class JsonSerializationException : JsonException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonSerializationException" /> class.
        /// </summary>
        /// <param name="fromType">Type of object attempted serializing to JSON.</param>
        internal JsonSerializationException(Type fromType) : base($"Error serializing {fromType.FullName} object into JSON.")
        {
        }
    }

    public class JsonNoDataException : JsonException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsonNoDataException" /> class.
        /// </summary>
        internal JsonNoDataException() : base("No data to deserialize.")
        {
        }
    }
}
