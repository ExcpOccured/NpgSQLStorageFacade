using Microsoft.Extensions.Options;
using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade
{
    public class StorageFacade : IStorageFacade
    {
        private readonly IStorageFacadeOptions _options;

        public StorageFacade(IOptions<IStorageFacadeOptions> options)
        {
            _options = options.Value;
        }
    }
}