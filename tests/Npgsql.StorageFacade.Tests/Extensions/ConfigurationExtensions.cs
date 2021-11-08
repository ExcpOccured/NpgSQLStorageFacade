using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Npgsql.StorageFacade.Tests.Attributes;

namespace Npgsql.StorageFacade.Tests.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static TOptions ReadConfiguredOptions<TOptions>(this IConfiguration configuration)
        {
            var configSection = typeof(TOptions).GetCustomAttributes<ConfigSectionAttribute>().FirstOrDefault();
            if (configSection == null)
                throw new InvalidOperationException($"Type {typeof(TOptions).FullName} is not marked with {nameof(ConfigSectionAttribute)}");

            var sectionName = configSection.Section;
            var section = configuration.GetSection(sectionName);
            if (section == null)
                throw new InvalidOperationException($"Section {sectionName} is absent");

            return section.Get<TOptions>();
        }
    }
}