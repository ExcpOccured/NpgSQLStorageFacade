using JetBrains.Annotations;
using Npgsql.StorageFacade.Sdk.Attributes;
using Npgsql.StorageFacade.Sdk.Models;

namespace Npgsql.StorageFacade.Sdk.Options
{
    [ConfigSection("StorageFacade")]
    public class StorageFacadeOptions
    {
        public int RetryCount { get; [UsedImplicitly] set; } = Constants.DefaultConnectionOpenRetryCount;

        public int DelayInSeconds { get; [UsedImplicitly] set; } = Constants.DefaultConnectionOpenSecondsDelay;
        
        public string ConnectionString { get; [UsedImplicitly]set; } = null!;
    }
}