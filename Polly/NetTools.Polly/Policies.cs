using Polly;
using Polly.Contrib.WaitAndRetry;

namespace NetTools.Polly;

public static class Policies
{
    /// <summary>
    ///     Create a policy that will retry the action immediately if it fails.
    /// </summary>
    /// <param name="retryEvaluation">Function to evaluate if the action failed (whether to trigger a retry).</param>
    /// <param name="retryLimit">How many times to retry the action.</param>
    /// <typeparam name="T">Exception thrown that will trigger a retry.</typeparam>
    /// <typeparam name="T2">Type of variable to pass into the retry evaluation function.</typeparam>
    /// <returns>A configured async policy to retry.</returns>
    public static IAsyncPolicy<T2> CreateImmediateRetryPolicy<T, T2>(Func<T2, bool> retryEvaluation, int retryLimit) where T : Exception
    {
        return Policy
            .Handle<T>()
            .OrResult(retryEvaluation)
            .RetryAsync(retryLimit);
    }

    /// <summary>
    ///     Create a policy that will retry the action immediately if it fails.
    /// </summary>
    /// <param name="retryEvaluation">Function to evaluate if the action failed.</param>
    /// <param name="retryLimit">How many times to retry the action.</param>
    /// <typeparam name="T">Type of variable to pass into the retry evaluation function.</typeparam>
    /// <returns>A configured async policy to retry.</returns>
    public static IAsyncPolicy<T> CreateImmediateRetryPolicy<T>(Func<T, bool> retryEvaluation, int retryLimit)
    {
        return Policy
            .HandleResult(retryEvaluation)
            .RetryAsync(retryLimit);
    }

    /// <summary>
    ///     Create a policy that will retry the action if it fails, with a constant delay between each retry.
    /// </summary>
    /// <param name="retryEvaluation">Function to evaluate if the action failed (whether to trigger a retry).</param>
    /// <param name="delayMilliseconds">Milliseconds to wait between each retry.</param>
    /// <param name="retryLimit">How many times to retry the action.</param>
    /// <typeparam name="T">Exception thrown that will trigger a retry.</typeparam>
    /// <typeparam name="T2">Type of variable to pass into the retry evaluation function.</typeparam>
    /// <returns>A configured async policy to retry.</returns>
    public static IAsyncPolicy<T2> CreateWaitingRetryPolicy<T, T2>(Func<T2, bool> retryEvaluation, int delayMilliseconds, int retryLimit) where T : Exception
    {
        return Policy
            .Handle<T>()
            .OrResult(retryEvaluation)
            .WaitAndRetryAsync(retryLimit, _ => TimeSpan.FromMilliseconds(delayMilliseconds));
    }

    /// <summary>
    ///     Create a policy that will retry the action if it fails, with a constant delay between each retry.
    /// </summary>
    /// <param name="retryEvaluation">Function to evaluate if the action failed (whether to trigger a retry).</param>
    /// <param name="delayMilliseconds">Milliseconds to wait between each retry.</param>
    /// <param name="retryLimit">How many times to retry the action.</param>
    /// <typeparam name="T">Type of variable to pass into the retry evaluation function.</typeparam>
    /// <returns>A configured async policy to retry.</returns>
    public static IAsyncPolicy<T> CreateWaitingRetryPolicy<T>(Func<T, bool> retryEvaluation, int delayMilliseconds, int retryLimit)
    {
        return Policy
            .HandleResult(retryEvaluation)
            .WaitAndRetryAsync(retryLimit, _ => TimeSpan.FromMilliseconds(delayMilliseconds));
    }

    /// <summary>
    ///     Create a policy that will retry the action if it fails, with a increasing delay between each retry.
    /// </summary>
    /// <param name="retryEvaluation">Function to evaluate if the action failed (whether to trigger a retry).</param>
    /// <param name="retryTimeSpans">List of timespans to wait between each retry.</param>
    /// <typeparam name="T">Exception thrown that will trigger a retry.</typeparam>
    /// <typeparam name="T2">Type of variable to pass into the retry evaluation function.</typeparam>
    /// <returns>A configured async policy to retry.</returns>
    public static IAsyncPolicy<T2> CreateBackOffRetryPolicy<T, T2>(Func<T2, bool> retryEvaluation, IEnumerable<TimeSpan> retryTimeSpans) where T : Exception
    {
        return Policy
            .Handle<T>()
            .OrResult(retryEvaluation)
            .WaitAndRetryAsync(retryTimeSpans);
    }

    /// <summary>
    ///     Create a policy that will retry the action if it fails, with a increasing delay between each retry.
    /// </summary>
    /// <param name="retryEvaluation">Function to evaluate if the action failed (whether to trigger a retry).</param>
    /// <param name="retryLimit">How many times to retry the action.</param>
    /// <typeparam name="T">Exception thrown that will trigger a retry.</typeparam>
    /// <typeparam name="T2">Type of variable to pass into the retry evaluation function.</typeparam>
    /// <returns>A configured async policy to retry.</returns>
    public static IAsyncPolicy<T2> CreateBackOffRetryPolicy<T, T2>(Func<T2, bool> retryEvaluation, int retryLimit) where T : Exception
    {
        var retryTimeSpans = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: retryLimit);
        return CreateBackOffRetryPolicy<T, T2>(retryEvaluation, retryTimeSpans);
    }

    /// <summary>
    ///     Create a policy that will retry the action if it fails, with a increasing delay between each retry.
    /// </summary>
    /// <param name="retryEvaluation">Function to evaluate if the action failed (whether to trigger a retry).</param>
    /// <param name="retryTimeSpans">List of timespans to wait between each retry.</param>
    /// <typeparam name="T">Type of variable to pass into the retry evaluation function.</typeparam>
    /// <returns>A configured async policy to retry.</returns>
    public static IAsyncPolicy<T> CreateBackOffRetryPolicy<T>(Func<T, bool> retryEvaluation, IEnumerable<TimeSpan> retryTimeSpans)
    {
        return Policy
            .HandleResult(retryEvaluation)
            .WaitAndRetryAsync(retryTimeSpans);
    }

    /// <summary>
    ///     Create a policy that will retry the action if it fails, with a increasing delay between each retry.
    /// </summary>
    /// <param name="retryEvaluation">Function to evaluate if the action failed (whether to trigger a retry).</param>
    /// <param name="retryLimit">How many times to retry the action.</param>
    /// <typeparam name="T">Type of variable to pass into the retry evaluation function.</typeparam>
    /// <returns>A configured async policy to retry.</returns>
    public static IAsyncPolicy<T> CreateBackOffRetryPolicy<T>(Func<T, bool> retryEvaluation, int retryLimit)
    {
        var retryTimeSpans = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: retryLimit);
        return CreateBackOffRetryPolicy<T>(retryEvaluation, retryTimeSpans);
    }
}
