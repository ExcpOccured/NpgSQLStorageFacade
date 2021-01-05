using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Npgsql.StorageFacade.SDK.Domain.Helpers
{
    public static class RetryTaskHelper
    {
        public static async Task RetryOnExceptionAsync(
            int times,
            int delayInSeconds,
            Func<Task> operation,
            ILogger logger)
        {
            await RetryOnExceptionAsync<Exception>(times, delayInSeconds, operation, logger);
        }

        private static async Task RetryOnExceptionAsync<TException>(
            int times,
            int delayInSeconds,
            Func<Task> operation,
            ILogger logger)
            where TException : Exception
        {
            if (times <= 0)
                throw new ArgumentOutOfRangeException(nameof(times));

            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    await operation();
                    break;
                }
                catch (TException exception)
                {
                    if (attempts == times)
                    {
                        throw;
                    }

                    await CreateDelayForException(times, attempts, delayInSeconds, logger, exception);
                }
            } while (true);
        }

        private static Task CreateDelayForException(
            int times,
            int attempts,
            int delayInSeconds,
            ILogger logger,
            Exception exception)
        {
            delayInSeconds += IncreasingDelayInSeconds(attempts);

            logger.LogWarning($"Exception on attempt {attempts} of {times}. " +
                              $"Will retry after sleeping for {delayInSeconds}.", exception);

            return Task.Delay(TimeSpan.FromSeconds(delayInSeconds));
        }

        private static int IncreasingDelayInSeconds(int failedAttempts)
        {
            if (failedAttempts <= 0) throw new ArgumentOutOfRangeException();

            //Sigmoid
            return Convert.ToInt32(Math.Round(1 / (1 + Math.Exp(-failedAttempts + 5)) * 100)) * 1000;
        }
    }
}