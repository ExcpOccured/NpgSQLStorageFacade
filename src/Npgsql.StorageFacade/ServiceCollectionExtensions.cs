using Microsoft.Extensions.DependencyInjection;
using Npgsql.StorageFacade.Sdk.Command;
using Npgsql.StorageFacade.Sdk.Connection;
using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNpgSqlStorageFacade<TOptions>(this IServiceCollection services,
            string optionsConfigSections)
            where TOptions : IStorageFacadeOptions
        {
            services.RegisterSdk();
            ConfigureStorageFacadeWithOptions(services);
        }

        private static void RegisterSdk(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INpgsqlCommandBuilder,
                Sdk.Command.NpgsqlCommandBuilder>();
            serviceCollection.AddSingleton<IConnectionManager, ConnectionManager>();
        }

        private static void ConfigureStorageFacadeWithOptions<TOptions>(
            IServiceCollection services)
        {
            services.AddSingleton<IStorageFacade>(serviceProvider =>
            {
                return new StorageFacade()
            });
        }
    }
}
