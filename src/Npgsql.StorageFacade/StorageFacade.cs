using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade
{
    public class StorageFacade : IStorageFacade
    {
        private readonly IStorageFacadeOptions _options;

        public StorageFacade(IStorageFacadeOptions options)
        {
            _options = options;
        }
    }
}