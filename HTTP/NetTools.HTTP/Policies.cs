using System.Net;
using Polly;

namespace NetTools.HTTP;

public static class Policies
{
    public static class Retry
    {
        public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicyForHttpException(int retryCount)
        {
            return Polly.Policies.Retry.CreateBackOffRetryPolicy<HttpRequestException, HttpResponseMessage>(_ => true, retryCount);
        }

        public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicyForHttpStatusCodeInRange(int minStatusCode, int maxStatusCode, int retryCount = 5)
        {
            var retryEvaluation = new Func<HttpResponseMessage, bool>(response => StatusCodes.StatusCodeBetween(response.StatusCode, minStatusCode, maxStatusCode));

            return Polly.Policies.Retry.CreateBackOffRetryPolicy<HttpRequestException, HttpResponseMessage>(retryEvaluation, retryCount);
        }

        public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicyForHttpStatusCodeOutsideRange(int minStatusCode, int maxStatusCode, int retryCount = 5)
        {
            var retryEvaluation = new Func<HttpResponseMessage, bool>(response => !StatusCodes.StatusCodeBetween(response.StatusCode, minStatusCode, maxStatusCode));

            return Polly.Policies.Retry.CreateBackOffRetryPolicy<HttpRequestException, HttpResponseMessage>(retryEvaluation, retryCount);
        }

        public static IAsyncPolicy<HttpResponseMessage> CreateRetryPolicyForHttpStatusCodeInList(IEnumerable<HttpStatusCode> statusCodes, int retryCount = 5)
        {
            var retryEvaluation = new Func<HttpResponseMessage, bool>(response => statusCodes.Contains(response.StatusCode));

            return Polly.Policies.Retry.CreateBackOffRetryPolicy<HttpRequestException, HttpResponseMessage>(retryEvaluation, retryCount);
        }
    }

    public static class Timeout
    {
        public static IAsyncPolicy CreateTimeoutPolicyForHttpCall(TimeSpan timeout)
        {
            return Polly.Policies.Timeout.CreateTimeoutPolicy(timeout);
        }
        
        public static IAsyncPolicy CreateTimeoutPolicyForHttpCall(int timeoutInMilliseconds)
        {
            return Polly.Policies.Timeout.CreateTimeoutPolicy(timeoutInMilliseconds);
        }
    }
}
