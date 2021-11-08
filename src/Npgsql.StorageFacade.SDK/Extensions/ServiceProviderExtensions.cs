using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Npgsql.StorageFacade.Sdk.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static TOptions GetOptionsValue<TOptions>(this IServiceProvider provider)
            where TOptions : class, new()
        {
            return provider.GetRequiredService<IOptions<TOptions>>().Value;
        }
    }
}