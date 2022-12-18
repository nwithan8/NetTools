using System.Net;
using Polly;

namespace NetTools.HTTP;

public class HttpDelegatingHandler : DelegatingHandler
{
    private IAsyncPolicy<HttpResponseMessage>? _retryPolicy;

    private IAsyncPolicy? _timeoutPolicy;

    /// <summary>
    ///     Configure this handler to automatically retry requests that fail with a transient error, up to a maximum number of times.
    /// </summary>
    /// <param name="maxRetries">Maximum number of times to retry.</param>
    /// <returns>This HttpDelegatingHandler instance.</returns>
    public HttpDelegatingHandler RetryOnHttpException(int maxRetries)
    {
        _retryPolicy = Policies.Retry.CreateRetryPolicyForHttpException(retryCount: maxRetries);
        return this;
    }

    /// <summary>
    ///     Configure this handler to automatically retry requests that return one of the specified HTTP status codes, up to a maximum number of times.
    /// </summary>
    /// <param name="statusCodes">List of status codes that, if one is returned by the request, will trigger a retry.</param>
    /// <param name="maxRetries">Maximum number of times to retry.</param>
    /// <returns>This HttpDelegatingHandler instance.</returns>
    public HttpDelegatingHandler RetryOnStatusCodes(IEnumerable<HttpStatusCode> statusCodes, int maxRetries)
    {
        _retryPolicy = Policies.Retry.CreateRetryPolicyForHttpStatusCodeInList(statusCodes: statusCodes, retryCount: maxRetries);
        return this;
    }
    
    public HttpDelegatingHandler TimeoutAfter(TimeSpan timeout)
    {
        _timeoutPolicy = Policies.Timeout.CreateTimeoutPolicyForHttpCall(timeout);
        return this;
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
        // wrap the timeout in the retry policy, so that the timeout is applied to each retry

        if (_retryPolicy != null)
        {
            if (_timeoutPolicy != null)
            {
                // both policies
                return await _retryPolicy.WrapAsync(_timeoutPolicy).ExecuteAsync(async () => await base.SendAsync(request, cancellationToken));
            }
            
            // just retry policy
            return await _retryPolicy.ExecuteAsync(async () => await base.SendAsync(request, cancellationToken));
        }
        
        
        if (_timeoutPolicy != null)
        {
            // just timeout policy
            return await _timeoutPolicy.ExecuteAsync(async () => await base.SendAsync(request, cancellationToken));
        }
        
        // no policies
        return await base.SendAsync(request, cancellationToken);
    }
}
