using Polly;

namespace NetTools.HTTP;

public static class RetryPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicyForHttpStatusCodeInRange(int minStatusCode, int maxStatusCode, int retryCount = 5)
    {
        var retryEvaluation = new Func<HttpResponseMessage, bool>(response => StatusCodes.StatusCodeBetween(response.StatusCode, minStatusCode, maxStatusCode));

        return Polly.Policies.CreateBackOffRetryPolicy<HttpRequestException, HttpResponseMessage>(retryEvaluation, retryCount);
    }

    public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicyForHttpStatusCodeOutsideRange(int minStatusCode, int maxStatusCode, int retryCount = 5)
    {
        var retryEvaluation = new Func<HttpResponseMessage, bool>(response => !StatusCodes.StatusCodeBetween(response.StatusCode, minStatusCode, maxStatusCode));

        return Polly.Policies.CreateBackOffRetryPolicy<HttpRequestException, HttpResponseMessage>(retryEvaluation, retryCount);
    }
}
