﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Npgsql.StorageFacade.Sdk.Models.Arguments;

namespace Npgsql.StorageFacade.Sdk.Command
{
    [PublicAPI]
    public interface INpgsqlCommandBuilder
    {
        NpgsqlCommand BuildCommand(
            Func<IEnumerable<ICommandArgument>, string> buildQueryDelegate,
            List<ICommandArgument> commandArguments,
            Func<string, bool>? validateCommandArgumentsDelegate = null);
    }
}