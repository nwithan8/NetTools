using NetTools.JSON;
using Newtonsoft.Json;

namespace NetTools.RestAPIClient;

/// <summary>
///     Class for any object that comes from or goes to the REST API.
/// </summary>
public abstract class BaseObject
{
    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        GetType() == obj?.GetType() && GetHashCode() == ((BaseObject)obj).GetHashCode();

    /// <inheritdoc />
#pragma warning disable CA1307 // Specify StringComparison
    public override int GetHashCode() => AsJson().GetHashCode() ^ GetType().GetHashCode();
#pragma warning restore CA1307 // Specify StringComparison

    /// <summary>
    ///     Compare two objects for equality.
    /// </summary>
    /// <param name="one">The first object in the comparison.</param>
    /// <param name="two">The second object in the comparison.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, false.</returns>
    public static bool operator ==(BaseObject? one, object? two)
    {
        if (one is null && two is null)
        {
            return true;
        }

#pragma warning disable IDE0046
        if (one is null || two is null)
#pragma warning restore IDE0046
        {
            return false;
        }

        return one.Equals(two);
    }

    /// <summary>
    ///     Compare two objects for equality.
    /// </summary>
    /// <param name="one">The first object in the comparison.</param>
    /// <param name="two">The second object in the comparison.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, false.</returns>
    public static bool operator !=(BaseObject? one, object? two) => !(one == two);

    /// <summary>
    ///     Get the JSON representation of this object instance.
    /// </summary>
    /// <returns>A JSON string representation of this object instance's attributes.</returns>
    protected string AsJson() => JsonSerialization.ConvertObjectToJson(this);

    /// <summary>
    ///     Gets this object as a JSON object (dictionary).
    /// </summary>
    /// <returns>A dictionary representation of this object's properties.</returns>
    public virtual Dictionary<string, object> AsDictionary() =>
        JsonConvert.DeserializeObject<Dictionary<string, object>>(AsJson())!;
}