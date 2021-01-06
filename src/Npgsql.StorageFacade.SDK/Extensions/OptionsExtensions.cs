using System;
using System.Reflection;
using Npgsql.StorageFacade.Sdk.Attributes;

namespace Npgsql.StorageFacade.Sdk.Extensions
{
    public static class OptionsExtensions
    {
        public static string? GetConfigSection(this Type optionsType)
        {
            var configSectionAttribute = optionsType.GetCustomAttribute<ConfigSectionAttribute>();
            return configSectionAttribute?.Section;
        }
    }
}