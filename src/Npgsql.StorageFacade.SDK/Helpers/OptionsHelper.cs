using System;
using Npgsql.StorageFacade.Sdk.Extensions;
using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade.Sdk.Helpers
{
    public static class OptionsHelper
    {
        public static TOptions GetOptionsWithValidation<TOptions>(this IServiceProvider provider)
            where TOptions : class, IOptions, new()
        {
            var options = provider.GetOptionsValue<TOptions>();

            if (!options.Validate(out var errors))
            {
                throw new ArgumentException(errors);
            }

            return options;
        }
    }
}