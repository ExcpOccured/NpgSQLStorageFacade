using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.StorageFacade.Sdk.Command;
using Npgsql.StorageFacade.Sdk.Connection;
using Npgsql.StorageFacade.Sdk.Helpers;
using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade
{
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        public static void AddNpgSqlStorageFacade<TOptions>(this IServiceCollection services)
            where TOptions : class, IStorageFacadeOptions, new()
        {
            services.RegisterSdk();
            services.RegisterFacade<TOptions>();
        }

        private static void RegisterSdk(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<INpgsqlCommandBuilder,
                Sdk.Command.NpgsqlCommandBuilder>();
            serviceCollection.AddSingleton<IConnectionManager, ConnectionManager>();
        }

        private static void RegisterFacade<TOptions>(
            this IServiceCollection services)
            where TOptions : class, IStorageFacadeOptions, new()
        {
            services.AddSingleton<IStorageFacade>(serviceProvider => 
                new StorageFacade(serviceProvider.GetOptionsWithValidation<TOptions>()));
        }
    }
}