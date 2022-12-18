using System.Net;

namespace NetTools.HTTP;

public class HttpClientBuilder
{
    private readonly HttpDelegatingHandler _handler;

    private Uri? _baseAddress;

    public HttpClientBuilder()
    {
        _handler = new HttpDelegatingHandler();
    }

    public HttpClientBuilder WithBaseAddress(string baseAddress)
    {
        _baseAddress = new Uri(baseAddress);
        return this;
    }
    
    public HttpClientBuilder RetryOnFailure(int retryCount)
    {
        _handler.RetryOnHttpException(retryCount);
        return this;
    }

    public HttpClientBuilder RetryOnStatusCodes(IEnumerable<HttpStatusCode> statusCodes, int retryCount)
    {
        _handler.RetryOnStatusCodes(statusCodes, retryCount);
        return this;
    }
    
    public HttpClientBuilder TimeoutAfter(TimeSpan timeout)
    {
        _handler.TimeoutAfter(timeout);
        return this;
    }

    public HttpClient Build()
    {
        var client = new HttpClient(_handler)
        {
            BaseAddress = _baseAddress
        };
        
        return client;
    }
}
