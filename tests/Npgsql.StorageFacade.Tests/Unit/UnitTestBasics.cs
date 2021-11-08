using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql.StorageFacade.Tests.Extensions;
using Npgsql.StorageFacade.Tests.Options;

namespace Npgsql.StorageFacade.Tests.Unit
{
    public abstract class UnitTestBasics
    {
        private static readonly ServiceProvider ServiceProvider;

        private static readonly IConfiguration Configuration;

        static UnitTestBasics()
        {
            //Hardcoded value, hidden(git ignore) for the purpose of data privacy
            const string settingsName = "tests.configure.json";

            IServiceCollection services = new ServiceCollection()
                .AddLogging()
                .AddOptions();

            var builder = new ConfigurationBuilder()
                .AddJsonFile(settingsName, false, false);

            Configuration = builder.Build();

            services.Configure<StorageFacadeTestOptions>(options => GetOptions<StorageFacadeTestOptions>());
            
            services.AddNpgSqlStorageFacade<StorageFacadeTestOptions>();
            
            ServiceProvider = services.BuildServiceProvider();
        }

        protected static ILogger<TLogger> GetLogger<TLogger>()
            where TLogger : class
        {
            var factory = ServiceProvider.GetService<ILoggerFactory>();
            return factory.CreateLogger<TLogger>();
        }

        private static TOptions GetOptions<TOptions>()
            where TOptions : class, new()
        {
            return Configuration.ReadConfiguredOptions<TOptions>();
        }

        protected static TService GetService<TService>(Type typeService)
            where TService : class
        {
            return (TService)ServiceProvider.GetRequiredService(typeService);
        }
    }
}