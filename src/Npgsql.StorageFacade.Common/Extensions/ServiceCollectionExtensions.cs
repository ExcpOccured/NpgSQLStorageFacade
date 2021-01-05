using Microsoft.Extensions.DependencyInjection;
using Npgsql.StorageFacade.Common.Services.Interfaces;

namespace Npgsql.StorageFacade.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterStorageFacade(this ServiceCollection serviceCollection)
        {
            RegisterInternalTypes(serviceCollection);
        }

        private static void RegisterInternalTypes(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INpgsqlCommandBuilder, Services.NpgsqlCommandBuilder>();
        }
    }
}