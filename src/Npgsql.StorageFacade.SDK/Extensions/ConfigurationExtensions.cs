using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Npgsql.StorageFacade.Sdk.Attributes;

namespace Npgsql.StorageFacade.Sdk.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T ReadConfiguredOptions<T>(this IConfiguration configuration)
        {
            var configSection = typeof(T).GetCustomAttributes<ConfigSectionAttribute>().FirstOrDefault();
            if (configSection == null)
                throw new InvalidOperationException($"Type {typeof(T).FullName} is not marked with {nameof(ConfigSectionAttribute)}");

            var sectionName = configSection.Section;
            var section = configuration.GetSection(sectionName);
            if (section == null)
                throw new InvalidOperationException($"Section {sectionName} is absent");

            return section.Get<T>();
        }
        
        
        public static TOptions TryReadConfiguredOptions<TOptions>(this IConfiguration configuration)
            where TOptions : class
        {
            try
            {
                return configuration.ReadConfiguredOptions<TOptions>();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}