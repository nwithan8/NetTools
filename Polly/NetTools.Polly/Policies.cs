using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Timeout;

namespace NetTools.Polly;

public static class Policies
{
    public static class Timeout
    {
        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeout">TimeSpan for timeout.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(TimeSpan timeout)
        {
            return Policy.TimeoutAsync(timeout);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeout">TimeSpan for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(TimeSpan timeout, TimeoutStrategy strategy)
        {
            return Policy.TimeoutAsync(timeout, strategy);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeout">TimeSpan for timeout.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(TimeSpan timeout, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(timeout, onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeout">TimeSpan for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(TimeSpan timeout, TimeoutStrategy strategy, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(timeout, strategy, onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="milliseconds">Milliseconds for timeout.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(int milliseconds)
        {
            return CreateTimeoutPolicy(new TimeSpan(0, 0, 0, 0, milliseconds));
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="milliseconds">Milliseconds for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(int milliseconds, TimeoutStrategy strategy)
        {
            return CreateTimeoutPolicy(new TimeSpan(0, 0, 0, 0, milliseconds), strategy);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="milliseconds">Milliseconds for timeout.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(int milliseconds, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return CreateTimeoutPolicy(new TimeSpan(0, 0, 0, 0, milliseconds), onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="milliseconds">Milliseconds for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(int milliseconds, TimeoutStrategy strategy, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return CreateTimeoutPolicy(new TimeSpan(0, 0, 0, 0, milliseconds), strategy, onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutTimeSpanProvider">Function to calculate TimeSpan for timeout.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<TimeSpan> timeoutTimeSpanProvider)
        {
            return Policy.TimeoutAsync(timeoutTimeSpanProvider);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutTimeSpanProvider">Function to calculate TimeSpan for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<TimeSpan> timeoutTimeSpanProvider, TimeoutStrategy strategy)
        {
            return Policy.TimeoutAsync(timeoutTimeSpanProvider, strategy);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutTimeSpanProvider">Function to calculate TimeSpan for timeout.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<TimeSpan> timeoutTimeSpanProvider, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(timeoutTimeSpanProvider, onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutTimeSpanProvider">Function to calculate TimeSpan for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<TimeSpan> timeoutTimeSpanProvider, TimeoutStrategy strategy, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(timeoutTimeSpanProvider, strategy, onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutMillisecondsProvider">Function to calculate milliseconds for timeout.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<int> timeoutMillisecondsProvider)
        {
            return Policy.TimeoutAsync(() => new TimeSpan(0, 0, 0, 0, timeoutMillisecondsProvider()));
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutMillisecondsProvider">Function to calculate milliseconds for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<int> timeoutMillisecondsProvider, TimeoutStrategy strategy)
        {
            return Policy.TimeoutAsync(() => new TimeSpan(0, 0, 0, 0, timeoutMillisecondsProvider()), strategy);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutMillisecondsProvider">Function to calculate milliseconds for timeout.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<int> timeoutMillisecondsProvider, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(() => new TimeSpan(0, 0, 0, 0, timeoutMillisecondsProvider()), onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutMillisecondsProvider">Function to calculate milliseconds for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<int> timeoutMillisecondsProvider, TimeoutStrategy strategy, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(() => new TimeSpan(0, 0, 0, 0, timeoutMillisecondsProvider()), strategy, onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutTimeSpanProvider">Function to calculate TimeSpan for timeout.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<Context, TimeSpan> timeoutTimeSpanProvider)
        {
            return Policy.TimeoutAsync(timeoutTimeSpanProvider);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutTimeSpanProvider">Function to calculate TimeSpan for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<Context, TimeSpan> timeoutTimeSpanProvider, TimeoutStrategy strategy)
        {
            return Policy.TimeoutAsync(timeoutTimeSpanProvider, strategy);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutTimeSpanProvider">Function to calculate TimeSpan for timeout.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<Context, TimeSpan> timeoutTimeSpanProvider, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(timeoutTimeSpanProvider, onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutTimeSpanProvider">Function to calculate TimeSpan for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<Context, TimeSpan> timeoutTimeSpanProvider, TimeoutStrategy strategy, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(timeoutTimeSpanProvider, strategy, onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutMillisecondsProvider">Function to calculate milliseconds for timeout.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<Context, int> timeoutMillisecondsProvider)
        {
            return Policy.TimeoutAsync(context => new TimeSpan(0, 0, 0, 0, timeoutMillisecondsProvider(context)));
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutMillisecondsProvider">Function to calculate milliseconds for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<Context, int> timeoutMillisecondsProvider, TimeoutStrategy strategy)
        {
            return Policy.TimeoutAsync(context => new TimeSpan(0, 0, 0, 0, timeoutMillisecondsProvider(context)), strategy);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutMillisecondsProvider">Function to calculate milliseconds for timeout.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<Context, int> timeoutMillisecondsProvider, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(context => new TimeSpan(0, 0, 0, 0, timeoutMillisecondsProvider(context)), onTimeoutAsync: onTimeoutAsync);
        }

        /// <summary>
        ///     Creates a timeout policy that will timeout after the specified time.
        /// </summary>
        /// <param name="timeoutMillisecondsProvider">Function to calculate milliseconds for timeout.</param>
        /// <param name="strategy">Timeout strategy.</param>
        /// <param name="onTimeoutAsync">Function to execute when a timeout occurs.</param>
        /// <returns>Configured policy.</returns>
        public static IAsyncPolicy CreateTimeoutPolicy(Func<Context, int> timeoutMillisecondsProvider, TimeoutStrategy strategy, Func<Context, TimeSpan, Task, Task> onTimeoutAsync)
        {
            return Policy.TimeoutAsync(context => new TimeSpan(0, 0, 0, 0, timeoutMillisecondsProvider(context)), strategy, onTimeoutAsync: onTimeoutAsync);
        }
    }

    public static class Retry
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
}
