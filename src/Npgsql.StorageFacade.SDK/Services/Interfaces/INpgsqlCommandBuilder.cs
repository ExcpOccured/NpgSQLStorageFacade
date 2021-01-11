using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Npgsql.StorageFacade.Sdk.Models.Arguments;

namespace Npgsql.StorageFacade.Sdk.Services.Interfaces
{
    [PublicAPI]
    public interface INpgsqlCommandBuilder
    {
        NpgsqlCommand BuildCommand(
            Func<IEnumerable<ICommandArgument>, NpgsqlCommand> buildCommandDelegate,
            List<ICommandArgument> commandArguments,
            Func<string, bool> validateCommandArgumentsDelegate = null);
    }
}