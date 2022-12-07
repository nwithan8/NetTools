using Polly;
using RestSharp;

namespace NetTools.HTTP.RestSharp;

public static class RetryPolicies
{
    public static IAsyncPolicy<RestResponse> CreateRetryPolicyForHttpStatusCodeInRange(int minStatusCode, int maxStatusCode, int retryCount = 5)
    {
        var retryEvaluation = new Func<RestResponse, bool>(response => HTTP.StatusCodes.StatusCodeBetween(response.StatusCode, minStatusCode, maxStatusCode));

        return Polly.Policies.CreateBackOffRetryPolicy(retryEvaluation, retryCount);
    }
    
    public static IAsyncPolicy<RestResponse<T>> CreateRetryPolicyForHttpStatusCodeInRange<T>(int minStatusCode, int maxStatusCode, int retryCount = 5)
    {
        var retryEvaluation = new Func<RestResponse, bool>(response => HTTP.StatusCodes.StatusCodeBetween(response.StatusCode, minStatusCode, maxStatusCode));

        return Polly.Policies.CreateBackOffRetryPolicy<RestResponse<T>>(retryEvaluation, retryCount);
    }

    public static IAsyncPolicy<RestResponse> CreateRetryPolicyForHttpStatusCodeOutsideRange(int minStatusCode, int maxStatusCode, int retryCount = 5)
    {
        var retryEvaluation = new Func<RestResponse, bool>(response => !HTTP.StatusCodes.StatusCodeBetween(response.StatusCode, minStatusCode, maxStatusCode));

        return Polly.Policies.CreateBackOffRetryPolicy(retryEvaluation, retryCount);
    }
    
    public static IAsyncPolicy<RestResponse<T>> CreateRetryPolicyForHttpStatusCodeOutsideRange<T>(int minStatusCode, int maxStatusCode, int retryCount = 5)
    {
        var retryEvaluation = new Func<RestResponse<T>, bool>(response => !HTTP.StatusCodes.StatusCodeBetween(response.StatusCode, minStatusCode, maxStatusCode));

        return Polly.Policies.CreateBackOffRetryPolicy<RestResponse<T>>(retryEvaluation, retryCount);
    }
}
