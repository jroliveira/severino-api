﻿namespace Severino.Infrastructure.Resilience
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    using Polly;
    using Polly.CircuitBreaker;
    using Polly.Retry;

    using Severino.Infrastructure.Resilience.Configurations;

    using static System.Threading.Tasks.Task;
    using static System.TimeSpan;

    using static Polly.Policy;

    using static Severino.Infrastructure.ErrorHandling.ExceptionHandler;
    using static Severino.Infrastructure.Logging.Logger;
    using static Severino.Infrastructure.Monad.Utils.Util;

    internal sealed class ResiliencePolicy
    {
        private readonly AsyncRetryPolicy? policy;

        public ResiliencePolicy(IOptions<ResilienceConfiguration> config)
        {
            if (!config.Value.IsEnabled())
            {
                return;
            }

            var (_, (retryCount, retryTimeInMs)) = config.Value;
            var sleepDurationProvider = GetSleepDurationProvider(retryTimeInMs);

            Handle<Exception>()
                .WaitAndRetry(retryCount, sleepDurationProvider);

            this.policy = Handle<BrokenCircuitException>()
                .Or<Exception>()
                .WaitAndRetryAsync(retryCount, sleepDurationProvider, WriteLog);
        }

        internal Task Execute(Func<Context?, Task> func) => this.Execute(new Dictionary<string, object>(), func);

        internal Task Execute(
            IDictionary<string, object> contextData,
            Func<Context?, Task> func) => this.Execute(contextData, _ =>
            {
                func(_);
                return FromResult(Unit());
            });

        internal Task<TResult> Execute<TResult>(Func<Context?, Task<TResult>> func) => this.Execute(new Dictionary<string, object>(), func);

        internal Task<TResult> Execute<TResult>(
            IDictionary<string, object> contextData,
            Func<Context?, Task<TResult>> func) => this.policy == default
                ? func(default)
                : this.policy.ExecuteAsync(func, contextData);

        private static Func<int, TimeSpan> GetSleepDurationProvider(int timeInMs) => count => FromMilliseconds(timeInMs * count);

        private static Task WriteLog(Exception exception, TimeSpan timeSpan, int retryCount, Context context)
        {
            var logInfo = new { RetryCount = retryCount, context.PolicyKey };

            if (exception == default)
            {
                LogError("A non success operation was received on retry count for policy key.", logInfo);
            }
            else
            {
                LogError("An exception occurred on retry count for policy key.", logInfo, HandleException(exception));
            }

            return CompletedTask;
        }
    }
}
