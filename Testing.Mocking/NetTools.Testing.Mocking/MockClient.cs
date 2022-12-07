using System.Net;
using System.Text.RegularExpressions;
using NetTools.HTTP;

namespace NetTools.Testing.Mocking;

public class MockRequestMatchRules
{
    internal HttpMethod Method { get; }

    internal string ResourceRegex { get; }

    public MockRequestMatchRules(HttpMethod method, string resourceRegex)
    {
        Method = method;
        ResourceRegex = resourceRegex;
    }
}

public class MockRequestResponseInfo
{
    internal HttpStatusCode StatusCode { get; }

    internal string? Content { get; }

    public MockRequestResponseInfo(HttpStatusCode statusCode, string? content = null, object? data = null)
    {
        StatusCode = statusCode;
        Content = content ?? (data != null ? JsonSerialization.ConvertObjectToJson(data) : string.Empty);
    }
}

public class MockRequest
{
    public MockRequestMatchRules MatchRules { get; }

    public MockRequestResponseInfo ResponseInfo { get; }

    public MockRequest(MockRequestMatchRules matchRules, MockRequestResponseInfo responseInfo)
    {
        MatchRules = matchRules;
        ResponseInfo = responseInfo;
    }
}

internal class MockClientHandler : DelegatingHandler
{
    private readonly List<MockRequest> _mockRequests;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MockClientHandler" /> class.
    /// </summary>
    /// <param name="innerHandler">Inner handler to also execute on requests.</param>
    /// <param name="mockRequests">List of requests to mock.</param>
    internal MockClientHandler(HttpMessageHandler innerHandler, List<MockRequest> mockRequests)
    {
        InnerHandler = innerHandler;
        _mockRequests = mockRequests;
    }

    /// <summary>
    ///     Override to alter the request-response behavior.
    ///     Record the request and response to the cassette.
    /// </summary>
    /// <param name="request">HttpRequestMessage object.</param>
    /// <param name="cancellationToken">CancellationToken object.</param>
    /// <returns>HttpResponseMessage object.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var mockRequest = FindMatchingMockRequest(request);

        if (mockRequest == null)
        {
            throw new Exception($"No matching mock request found for: {request.Method.ToString().ToUpper()} {request.RequestUri}");
        }

        return new HttpResponseMessage(mockRequest.ResponseInfo.StatusCode)
        {
            Content = new StringContent(mockRequest.ResponseInfo.Content ?? string.Empty)
        };
    }

    private MockRequest? FindMatchingMockRequest(HttpRequestMessage request)
    {
        foreach (var mockRequest in _mockRequests)
        {
            if (mockRequest.MatchRules.Method != request.Method)
            {
                continue;
            }

            if (!EndpointMatches(request.RequestUri, mockRequest.MatchRules.ResourceRegex))
            {
                continue;
            }

            return mockRequest;
        }

        return null;
    }

    private static bool EndpointMatches(Uri? endpoint, string pattern)
    {
        if (endpoint == null)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(endpoint.AbsoluteUri, pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Singleline, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}

public class MockClient : HttpClient
{
    public MockClient(List<MockRequest> mockRequests, HttpMessageHandler? innerHandler = null) : base(new MockClientHandler(innerHandler ?? new HttpClientHandler(), mockRequests))
    {
    }
}
