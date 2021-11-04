using System.Text;
using JetBrains.Annotations;
using Npgsql.StorageFacade.Sdk.Models;

namespace Npgsql.StorageFacade.Sdk.Options
{
    public class StorageFacadeOptions : IStorageFacadeOptions, IOptions
    {
        public int RetryCount { get; [UsedImplicitly] set; } = Constants.DefaultConnectionOpenRetryCount;

        public int DelayInMilliseconds { get; [UsedImplicitly] set; } = Constants.DefaultConnectionOpenSecondsDelay;
        
        public string ConnectionString { get; [UsedImplicitly]set; } = null!;
        
        
        public bool Validate(out string errors)
        {
            const string greaterThanZeroTemplateMessage = "must be greater than zero!";

            const string retryCountErrorMessage = "RetryCount" + greaterThanZeroTemplateMessage;

            const string delayInMillisecondsErrorMessage = "DelayInMilliseconds" + greaterThanZeroTemplateMessage;

            const string connectionStringErrorMessage = "Connection string must be initialized and shouldn't be empty!";

            var maxErrorsStringLength = retryCountErrorMessage.Length
                                        + delayInMillisecondsErrorMessage.Length
                                        + connectionStringErrorMessage.Length;
            
            // Avoiding reallocation's
            var errorsBuilder = new StringBuilder(maxErrorsStringLength);

            if (string.IsNullOrEmpty(ConnectionString))
            {
                errorsBuilder.AppendLine(connectionStringErrorMessage);
            }

            if (RetryCount == default)
            {
                errorsBuilder.AppendLine(retryCountErrorMessage);
            }

            if (DelayInMilliseconds == default)
            {
                errorsBuilder.AppendLine(delayInMillisecondsErrorMessage);
            }

            if (errorsBuilder.Length == default)
            {
                errors = string.Empty;
                return true;
            }

            errors = errorsBuilder.ToString();
            return false;
        }
    }
}