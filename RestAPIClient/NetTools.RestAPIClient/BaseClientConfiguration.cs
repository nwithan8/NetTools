using NetTools.Common;
using NetTools.HTTP;

namespace NetTools.RestAPIClient;

public abstract class BaseClientConfiguration : IDisposable
{
    /// <summary>
    ///     The API base URI.
    /// </summary>
    // This cannot be changed after the client has been initialized.
    public string ApiBase { get; set; }

    /// <summary>
    ///     A custom HttpClient to use for requests.
    /// </summary>
    // This cannot be changed after the client has been initialized, and is stored for reference only.
    public HttpClient? CustomHttpClient { get; set; } // default to null if not specified by the user

    /// <summary>
    ///     The timeout to use for requests.
    /// </summary>
    // This cannot be changed after the client has been initialized.
    public TimeSpan Timeout { get; set; } =
        TimeSpan.FromSeconds(60); // default to 60 seconds if not specified by the user
    
    /// <summary>
    ///     A set of <see cref="Hooks"/> to use for requests.
    /// </summary>
    // This cannot be changed after the client has been initialized.
    public Hooks Hooks { get; set; } = new();

    /// <summary>
    ///     An asynchronous method to call when a request fails with an error.
    ///     Method accepts a <see cref="HttpResponseMessage"/> and returns a <see cref="Task"/>.
    /// </summary>
    public Func<HttpResponseMessage, Task>? OnRequestError { get; set; } = async response =>
    {
        var content = await response.Content.ReadAsStringAsync();
        throw new ApiException($"Request failed with status code {response.StatusCode}: {content}");
    };

    /// <summary>
    ///     Gets the HttpClient to use for requests.
    ///     This is the HttpClient with the connect timeout set.
    /// </summary>
    // This is the prepared HttpClient that is actually used to make requests, will be initialized when the client is initialized (will never be null).
    internal HttpClient? PreparedHttpClient;

    /// <summary>
    ///     Gets the authentication to use for requests.
    /// </summary>
    // This cannot be changed after the client has been initialized.
    internal IAuthentication? Authentication;

    /// <summary>
    ///    The .NET version of the current application.
    /// </summary>
    private readonly string _dotNetVersion;

    /// <summary>
    ///    The version of this library.
    /// </summary>
    private readonly string _libraryVersion;

    /// <summary>
    ///    The architecture of the current application's operating system.
    /// </summary>
    private readonly string _osArch;

    /// <summary>
    ///     The name of the current application's operating system.
    /// </summary>
    private readonly string _osName;

    /// <summary>
    ///     The version of the current application's operating system.
    /// </summary>
    private readonly string _osVersion;

    /// <summary>
    ///     Get the User-Agent string to use for a request.
    ///     Override this method to implement custom User-Agent strings.
    /// </summary>
    /// <returns>The prepared User-Agent string.</returns>
    protected virtual string GetUserAgent() =>
        $"NetTools.RestAPIClient/{_libraryVersion} .NET/{_dotNetVersion} OS/{_osName} OSVersion/{_osVersion} OSArch/{_osArch}";

    /// <summary>
    ///     Gets the headers to use for a request (including User-Agent and optional authentication headers). 
    ///     Override this method to add additional headers (call the base method to get the default headers).
    /// </summary>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of HTTP headers.</returns>
    internal virtual Dictionary<string, string> GetHeaders()
    {
        var headers = new Dictionary<string, string>
        {
            { "User-Agent", GetUserAgent() }
        };

        if (Authentication?.IsHeaderBased != true) return headers;

        Dictionary<string, string> authHeaders = Authentication.AuthenticationPair;
        foreach (KeyValuePair<string, string> pair in authHeaders)
        {
            headers.Add(pair.Key, pair.Value);
        }

        return headers;
    }

    /// <summary>
    ///     Inject any authentication query parameter pairs into the provided parameter set.
    /// </summary>
    /// <param name="parameters">A <see cref="Dictionary{TKey,TValue}"/> set of parameters to inject authentication-related query parameters into, if applicable.</param>
    /// <returns>A <see cref="Dictionary{TKey,TValue}"/> of the original parameters, plus any applicable authentication-related query parameter pairs.</returns>
    internal Dictionary<string, object> AddOptionalAuthenticationParameters(
        Dictionary<string, object>? parameters)
    {
        parameters ??= new Dictionary<string, object>();
        
        if (Authentication?.IsQueryParameterBased != true) return parameters;

        Dictionary<string, string> authQueryParameters = Authentication.AuthenticationPair;
        foreach (KeyValuePair<string, string> pair in authQueryParameters)
        {
            parameters.Add(pair.Key, pair.Value);
        }

        return parameters;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseClientConfiguration"/> class.
    /// </summary>
    /// <param name="authentication">Optional <see cref="IAuthentication"/> to use for requests.</param>
    protected BaseClientConfiguration(IAuthentication? authentication = null)
    {
        // Required constructor parameters
        Authentication = authentication;

        // Optional constructor parameters with defaults, set by the constructor

        // Calculate the properties that are used in the User-Agent string once and store them
        _libraryVersion = RuntimeInfo.ApplicationInfo.ApplicationVersion;
        _dotNetVersion = RuntimeInfo.ApplicationInfo.DotNetVersion;
        _osName = RuntimeInfo.OperationSystemInfo.Name;
        _osVersion = RuntimeInfo.OperationSystemInfo.Version;
        _osArch = RuntimeInfo.OperationSystemInfo.Architecture;
    }

    /// <summary>
    ///     Sets up the HTTP client.
    ///     Because we need to wait for construction to finish, we have to do this in a separate method.
    /// </summary>
    internal void SetUp()
    {
        // Prepare the HttpClient
        PreparedHttpClient =
            CustomHttpClient ?? new HttpClient(); // copy the custom HttpClient if it exists, otherwise create a new one
        PreparedHttpClient.Timeout = Timeout;
    }

    /// <summary>
    ///     Evaluate whether the specified object is equal to the current object, based on the authentication and API base URI.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj) =>
        obj is BaseClientConfiguration other && Authentication == other.Authentication && ApiBase == other.ApiBase;

    /// <summary>
    ///     Whether this object has been disposed or not.
    /// </summary>
    private bool _isDisposed;

    /// <summary>
    ///     Dispose of this object.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Dispose of this object.
    ///     Override this method to dispose of additional resources in derived classes.
    /// </summary>
    /// <param name="disposing">Whether this object is being disposed.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            // Dispose managed state (managed objects).

            // dispose of the prepared HTTP client
            PreparedHttpClient?.Dispose();

            // dispose of the user-provided HTTP client
            CustomHttpClient?.Dispose();
        }

        // Free native resources (unmanaged objects) and override a finalizer below.
        _isDisposed = true;
    }

    /// <summary>
    ///     Finalizes an instance of the <see cref="BaseClientConfiguration"/> class.
    /// </summary>
    ~BaseClientConfiguration()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        Dispose(disposing: false);
    }
}