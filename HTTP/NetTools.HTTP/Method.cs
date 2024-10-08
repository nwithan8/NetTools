namespace NetTools.HTTP;

/// <summary>
///     Enum representing the available HTTP methods.
/// </summary>
public class Method
{
    /// <summary>
    ///     The <see cref="HttpMethod"/> associated with this enum.
    /// </summary>
    internal HttpMethod HttpMethod { get; }

    /// <summary>
    ///     HTTP GET method.
    /// </summary>
    public static readonly Method Get = new Method(HttpMethod.Get);

    /// <summary>
    ///     HTTP POST method.
    /// </summary>
    public static readonly Method Post = new Method(HttpMethod.Post);

    /// <summary>
    ///     HTTP PUT method.
    /// </summary>
    public static readonly Method Put = new Method(HttpMethod.Put);

    /// <summary>
    ///     HTTP DELETE method.
    /// </summary>
    public static readonly Method Delete = new Method(HttpMethod.Delete);

    /// <summary>
    ///     HTTP PATCH method.
    /// </summary>
    public static readonly Method Patch = new Method(new HttpMethod("PATCH"));

    /// <summary>
    ///     Initializes a new instance of the <see cref="Method"/> class.
    /// </summary>
    /// <param name="httpMethod">The <see cref="HttpMethod"/> to associate with this enum.</param>
    private Method(HttpMethod httpMethod)
    {
        HttpMethod = httpMethod;
    }

    public HttpMethod ToHttpMethod() => HttpMethod;
    
    public override string ToString() => HttpMethod.Method;
    
    public static implicit operator HttpMethod(Method method) => method.HttpMethod;
    
    public static implicit operator Method(HttpMethod method) => new(method);
    
    public static implicit operator Method(string method) => new(new HttpMethod(method));
    
    public static implicit operator string(Method method) => method.HttpMethod.Method;
}