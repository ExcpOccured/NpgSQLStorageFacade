﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql.StorageFacade.Sdk.Extensions;

namespace Npgsql.StorageFacade.Tests.Unit
{
    public abstract class UnitTestBasics
    {
        private static readonly ServiceProvider ServiceProvider;

        private static readonly IConfiguration Configuration;

        static UnitTestBasics()
        {
            const string settingsName = "tests.configure.json";

            var serviceCollection = new ServiceCollection()
                .AddLogging()
                .AddOptions();

            //serviceCollection.RegisterStorageFacade();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var builder = new ConfigurationBuilder()
                .AddJsonFile(settingsName, false, true);

            Configuration = builder.Build();
        }

        protected static ILogger<TLogger> GetLogger<TLogger>()
            where TLogger : class
        {
            var factory = ServiceProvider.GetService<ILoggerFactory>();
            return factory.CreateLogger<TLogger>();
        }

        protected static T GetOptions<T>()
            where T : class, new()
        {
            return Configuration.ReadConfiguredOptions<T>();
        }

        protected static TService GetService<TService>(Type typeService)
            where TService : class
        {
            return (TService)ServiceProvider.GetService(typeService);
        }
    }
}