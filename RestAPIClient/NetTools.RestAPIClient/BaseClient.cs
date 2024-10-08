using NetTools.HTTP;
using NetTools.JSON;
using NetTools.RestAPIClient.Http;
using NetTools.RestAPIClient.Parameters;

namespace NetTools.RestAPIClient;

public abstract class BaseClient : IDisposable
{
    /// <summary>
    ///     The configuration for this client. This is immutable once set and is not accessible to end-users.
    /// </summary>
    private readonly BaseClientConfiguration _configuration;

    /// <summary>
    ///     Gets the authentication used by this client.
    /// </summary>
    public IAuthentication? AuthenticationInUse
    {
        get => _configuration
            .Authentication; // public read-only property so users can audit the authentication used by the client
        internal set =>
            _configuration.Authentication =
                value; // internal setter so the library can alter this client's authentication when we need to
    }

    /// <summary>
    ///     Gets the base URL used by this client.
    /// </summary>
    public string ApiBaseInUse =>
        _configuration.ApiBase; // public read-only property so users can audit the base URL used by the client

    /// <summary>
    ///     Gets the timeout used by this client.
    /// </summary>
    public TimeSpan Timeout =>
        _configuration.Timeout; // public read-only property so users can audit the connect timeout used by the client

    /// <summary>
    ///     Gets the custom <see cref="System.Net.Http.HttpClient"/> used by this client.
    /// </summary>
    public HttpClient? CustomHttpClient =>
        _configuration
            .CustomHttpClient; // public read-only property so users can audit the custom HTTP client they set to be used by the client

    /// <summary>
    ///     Gets the <see cref="Hooks"/> used by this client.
    /// </summary>
    public Hooks Hooks
    {
        get => _configuration.Hooks; // public read-only property so users can audit the hooks used by the client
        set => _configuration.Hooks = value; // public setter so users can set the hooks used by the client
    }

    /// <summary>
    ///     Gets the prepared <see cref="System.Net.Http.HttpClient"/> used by this client for all API calls.
    ///     This is the actual client used to make requests, is inaccessible to end-users, and will never be null.
    /// </summary>
    private HttpClient HttpClient => _configuration.PreparedHttpClient!;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseClient"/> class.
    /// </summary>
    /// <param name="configuration"><see cref="BaseClientConfiguration"/> to use for this client.</param>
    protected BaseClient(BaseClientConfiguration configuration)
    {
        _configuration = configuration;

        // set up the configuration, needed to finalize the HttpClient used to make requests
        _configuration.SetUp();
    }

    /// <summary>
    ///     Execute an HTTP request.
    /// </summary>
    /// <param name="request"><see cref="HttpRequestMessage"/> to execute.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <returns>An <see cref="HttpResponseMessage"/> object.</returns>
    public virtual async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // This method actually executes the request and returns the response.
        // Everything up to this point has been pre-request, and everything after this point is post-request.
        // This method is "virtual" so it can be overridden (i.e. by a MockClient in testing to avoid making actual HTTP requests).

        // try to execute the request, catch and rethrow an HTTP timeout exception, all other exceptions are thrown as-is
        try
        {
            // generate a UUID and starting timestamp for this request
            var requestId = Guid.NewGuid();
            var requestTimestamp = Environment.TickCount;
            // if a pre-request event has been set, invoke it
            Hooks.OnRequestExecuting?.Invoke(this,
                new OnRequestExecutingEventArgs(request, requestTimestamp, requestId));
            // execute the request
            var response = await HttpClient.SendAsync(request, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            // if a post-request event has been set, invoke it
            var responseTimestamp = Environment.TickCount;
            Hooks.OnRequestResponseReceived?.Invoke(this,
                new OnRequestResponseReceivedEventArgs(response, requestTimestamp, responseTimestamp, requestId));

            return response;
        }
        catch (TaskCanceledException e)
        {
            throw;
        }
    }

    /// <summary>
    ///     Execute a request against the REST API.
    /// </summary>
    /// <param name="method">HTTP method to use for the request.</param>
    /// <param name="endpoint">API endpoint to use for the request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <param name="rootElement">Optional root element for the resultant JSON to begin deserialization at.</param>
    /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
    /// <returns>An instance of a T-type object.</returns>
    /// <exception cref="JsonDeserializationException">If JSON deserialization of response fails.</exception>
    public async Task<T> RequestAsync<T>(Method method, string endpoint, CancellationToken cancellationToken = default,
        string? rootElement = null) where T : class =>
        await RequestAsync<T>(cancellationToken, method, endpoint, null, rootElement);

    /// <summary>
    ///     Execute a request against the REST API.
    /// </summary>
    /// <param name="method">HTTP method to use for the request.</param>
    /// <param name="endpoint">API endpoint to use for the request.</param>
    /// <param name="parameters">Parameters to use for the request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <param name="rootElement">Optional root element for the resultant JSON to begin deserialization at.</param>
    /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
    /// <returns>An instance of a T-type object.</returns>
    /// <exception cref="JsonDeserializationException">If JSON deserialization of response fails.</exception>
    public async Task<T> RequestAsync<T>(Method method, string endpoint, IBaseParameters parameters,
        CancellationToken cancellationToken = default, string? rootElement = null) where T : class =>
        await RequestAsync<T>(cancellationToken, method, endpoint, parameters.ToDictionary(), rootElement);

    /// <summary>
    ///     Execute a request against the REST API.
    /// </summary>
    /// <param name="method">HTTP method to use for the request.</param>
    /// <param name="endpoint">API endpoint to use for the request.</param>
    /// <param name="parameters">Parameters to use for the request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <param name="rootElement">Optional root element for the resultant JSON to begin deserialization at.</param>
    /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
    /// <returns>An instance of a T-type object.</returns>
    /// <exception cref="JsonDeserializationException">If JSON deserialization of response fails.</exception>
    public async Task<T> RequestAsync<T>(Method method, string endpoint, Dictionary<string, object> parameters,
        CancellationToken cancellationToken = default, string? rootElement = null) where T : class =>
        await RequestAsync<T>(cancellationToken, method, endpoint, parameters, rootElement);

    /// <summary>
    ///     Execute a request against the REST API.
    /// </summary>
    /// <param name="method">HTTP method to use for the request.</param>
    /// <param name="endpoint">API endpoint to use for the request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <param name="parameters">Optional parameters to use for the request.</param>
    /// <param name="rootElement">Optional root element for the resultant JSON to begin deserialization at.</param>
    /// <typeparam name="T">Type of object to deserialize response data into.</typeparam>
    /// <returns>An instance of a T-type object.</returns>
    /// <exception cref="JsonDeserializationException">If JSON deserialization of response fails.</exception>
    private async Task<T> RequestAsync<T>(CancellationToken cancellationToken, Method method, string endpoint,
        Dictionary<string, object>? parameters, string? rootElement)
        where T : class
    {
        // Build the request
        var headers = _configuration.GetHeaders();
        var request = new Request(ApiBaseInUse, endpoint, method, parameters, headers);

        // Execute the request
        var response = await ExecuteRequest(request.AsHttpRequestMessage(), cancellationToken);

        // Check the response's status code
        if (response.ReturnedError())
        {
            await _configuration.OnRequestError!(response);
            return null;
        }

        // Prepare the list of root elements to use during deserialization
        List<string>? rootElements = null;
        if (rootElement != null)
        {
            rootElements = new List<string> { rootElement };
        }

        // Deserialize the response into an object
        var resource = await JsonSerialization.ConvertJsonToObject<T>(response, null, rootElements);

        // Dispose of the request and response
        request.Dispose();
        response.Dispose();

#pragma warning disable IDE0270 // Simplify null check
        if (resource is null)
        {
            // Object deserialization failed
            throw new JsonDeserializationException(typeof(T));
        }
#pragma warning restore IDE0270

        return resource;
    }

    /// <summary>
    ///     Execute a request against the REST API.
    /// </summary>
    /// <param name="method">HTTP method to use for the request.</param>
    /// <param name="endpoint">API endpoint to use for the request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <returns><c>true</c> if the request was successful, <c>false</c> otherwise.</returns>
    public async Task<bool> RequestAsync(Method method, string endpoint, CancellationToken cancellationToken) =>
        await RequestAsync(cancellationToken, method, endpoint, null);

    /// <summary>
    ///     Execute a request against the REST API.
    /// </summary>
    /// <param name="method">HTTP method to use for the request.</param>
    /// <param name="endpoint">API endpoint to use for the request.</param>
    /// <param name="parameters">Parameters to use for the request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <returns><c>true</c> if the request was successful, <c>false</c> otherwise.</returns>
    public async Task<bool> RequestAsync(Method method, string endpoint, IBaseParameters parameters,
        CancellationToken cancellationToken) =>
        await RequestAsync(cancellationToken, method, endpoint, parameters.ToDictionary());

    /// <summary>
    ///     Execute a request against the REST API.
    /// </summary>
    /// <param name="method">HTTP method to use for the request.</param>
    /// <param name="endpoint">API endpoint to use for the request.</param>
    /// <param name="parameters">Parameters to use for the request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <returns><c>true</c> if the request was successful, <c>false</c> otherwise.</returns>
    public async Task<bool> RequestAsync(Method method, string endpoint, Dictionary<string, object> parameters,
        CancellationToken cancellationToken) => await RequestAsync(cancellationToken, method, endpoint, null);

    /// <summary>
    ///     Execute a request against the EasyPost API.
    /// </summary>
    /// <param name="method">HTTP <see cref="Method"/> to use for the request.</param>
    /// <param name="endpoint">EasyPost API endpoint to use for the request.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
    /// <param name="parameters">Optional parameters to use for the request.</param>
    /// <returns><c>true</c> if the request was successful, <c>false</c> otherwise.</returns>
    // ReSharper disable once UnusedMethodReturnValue.Global
#pragma warning disable CA1068
    private async Task<bool> RequestAsync(CancellationToken cancellationToken, Method method, string endpoint,
        Dictionary<string, object>? parameters)
#pragma warning restore CA1068
    {
        // Build the request
        var headers = _configuration.GetHeaders();
        Request request = new(ApiBaseInUse, endpoint, method, parameters, headers);

        // Execute the request
        var response = await ExecuteRequest(request.AsHttpRequestMessage(), cancellationToken);

        // Check whether the HTTP request produced an error (3xx, 4xx or 5xx status code) or not
        var errorRaised = response.ReturnedNoError();

        // Dispose of the request and response
        request.Dispose();
        response.Dispose();

        return errorRaised;
    }

    /// <summary>
    ///     Compare this <see cref="BaseClient"/> to another object for equality.
    /// </summary>
    /// <param name="obj">An object to compare this client against.</param>
    /// <returns><c>true</c> if the two objects are equal, <c>false</c> otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is BaseClient client && _configuration.Equals(client._configuration);


    /// <summary>
    ///     Get the hash code for this <see cref="BaseClient"/>.
    /// </summary>
    /// <returns>The hash code for this <see cref="BaseClient"/>.</returns>
    public override int GetHashCode() => _configuration.GetHashCode();

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
    /// </summary>
    /// <param name="disposing">Whether this object is being disposed.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        if (disposing)
        {
            // Dispose managed state (managed objects).

            // Dispose the configuration
            _configuration.Dispose();
        }

        // Free native resources (unmanaged objects) and override a finalizer below.
        _isDisposed = true;
    }

    /// <summary>
    ///     Finalizes an instance of the <see cref="BaseClient"/> class.
    /// </summary>
    ~BaseClient()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        Dispose(disposing: false);
    }
}