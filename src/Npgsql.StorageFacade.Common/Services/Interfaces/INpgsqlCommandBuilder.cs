using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Npgsql.StorageFacade.Common.Models.Arguments;

namespace Npgsql.StorageFacade.Common.Services.Interfaces
{
    [PublicAPI]
    public interface INpgsqlCommandBuilder
    {
        NpgsqlCommand BuildCommand(
            Func<IEnumerable<ICommandArgument>, string> buildQueryDelegate,
            List<ICommandArgument> commandArguments,
            Func<string, bool> validateCommandArgumentsDelegate = null);
    }
}