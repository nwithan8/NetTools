namespace NetTools.RestAPIClient.Parameters
{
    /// <summary>
    ///     The base interface for all objects that can be used as a parameter in Parameters.
    /// </summary>
    public interface IParameter
    {
    }
    
    /// <summary>
    ///     Base interface for all REST API parameters.
    /// </summary>
    public interface IBaseParameters
    {
        /// <summary>
        ///     Convert this object to a <see cref="Dictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/>.</returns>
        public Dictionary<string, object> ToDictionary();

        /// <summary>
        ///     Convert this object to a sub-<see cref="Dictionary{TKey,TValue}"/> based on the parent parameter object type.
        /// </summary>
        /// <param name="parentParameterObjectType">The <see cref="Type"/> of the parent parameter object.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/>.</returns>
        public Dictionary<string, object> ToSubDictionary(Type parentParameterObjectType);
    }
}