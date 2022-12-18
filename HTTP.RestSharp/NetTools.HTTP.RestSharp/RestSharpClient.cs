using System.Net;
using RestSharp;

namespace NetTools.HTTP.RestSharp;

public class RestSharpClientBuilder
{
    private readonly HttpDelegatingHandler _handler;

    private RestClientOptions _options = new RestClientOptions();

    private HttpClient? _httpClient;

    private bool _disposeHttpClient = true;

    private HttpMessageHandler? _httpMessageHandler;

    private bool _disposeHttpMessageHandler;

    public RestSharpClientBuilder()
    {
        _handler = new HttpDelegatingHandler();
    }

    public RestSharpClientBuilder RetryOnFailure(int retryCount)
    {
        _handler.RetryOnHttpException(retryCount);
        return this;
    }

    public RestSharpClientBuilder RetryOnStatusCodes(IEnumerable<HttpStatusCode> statusCodes, int retryCount)
    {
        _handler.RetryOnStatusCodes(statusCodes, retryCount);
        return this;
    }
    
    public RestSharpClientBuilder TimeoutAfter(TimeSpan timeout)
    {
        _handler.TimeoutAfter(timeout);
        return this;
    }

    public RestSharpClientBuilder WithOptions(RestClientOptions options)
    {
        _options = options;
        return this;
    }

    public RestSharpClientBuilder WithInnerHttpClient(HttpClient httpClient, bool disposeHttpClient = false)
    {
        _httpClient = httpClient;
        _disposeHttpClient = disposeHttpClient;
        return this;
    }

    public RestSharpClientBuilder WithInnerHandler(HttpMessageHandler handler, bool disposeHandler = true)
    {
        _httpMessageHandler = handler;
        _disposeHttpMessageHandler = disposeHandler;
        return this;
    }

    private HttpClient BuildHttpClient()
    {
        if (_httpClient != null)
        {
            // User has set a specific HttpClient, use it
            return _httpClient;
        }

        if (_httpMessageHandler != null)
        {
            // User has set a specific HttpMessageHandler, use it
            return new HttpClient(_httpMessageHandler, _disposeHttpMessageHandler);
        }

        // No specific HttpClient or HttpMessageHandler, build one
        return new HttpClient(_handler, _disposeHttpMessageHandler);
    }

    public RestClient Build()
    {
        var httpClient = BuildHttpClient();
        return new RestClient(httpClient, _options, _disposeHttpClient);
    }
}
