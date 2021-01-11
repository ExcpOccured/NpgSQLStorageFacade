using System;
using System.Collections.Generic;
using Npgsql.StorageFacade.Sdk.Extensions;
using Npgsql.StorageFacade.Sdk.Models.Arguments;
using Npgsql.StorageFacade.Sdk.Services.Interfaces;

namespace Npgsql.StorageFacade.Sdk.Services
{
    public class NpgsqlCommandBuilder : INpgsqlCommandBuilder
    {
        public NpgsqlCommand BuildCommand(
            Func<IEnumerable<ICommandArgument>, NpgsqlCommand> buildCommandDelegate,
            List<ICommandArgument> commandArguments,
            Func<string, bool> validateCommandArgumentsDelegate = null)
        {
            var command = buildCommandDelegate.Invoke(commandArguments);

            if (!validateCommandArgumentsDelegate?.Invoke(command.CommandText) ?? true)
            {
                throw new ArgumentException(nameof(command.CommandText));
            }

            command.Parameters.AddRangeWithArguments(commandArguments);

            return command;
        }
    }
}