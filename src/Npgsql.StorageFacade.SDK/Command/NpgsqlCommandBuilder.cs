﻿using System;
using System.Collections.Generic;
using Npgsql.StorageFacade.Sdk.Models.Arguments;

namespace Npgsql.StorageFacade.Sdk.Command
{
    public class NpgsqlCommandBuilder : INpgsqlCommandBuilder
    {
        public NpgsqlCommand BuildCommand(
            Func<IEnumerable<ICommandArgument>, string> buildQueryDelegate,
            List<ICommandArgument> commandArguments,
            Func<string, bool>? validateCommandArgumentsDelegate = null)
        {
            var queryString = buildQueryDelegate.Invoke(commandArguments);
            validateCommandArgumentsDelegate?.Invoke(queryString);

            var sqlCommand = new NpgsqlCommand
            {
                CommandText = queryString
            };

            foreach (var argument in commandArguments)
            {
                sqlCommand.Parameters.AddWithValue(argument.ArgumentVariableName, argument.ArgumentVariableValue);
            }

            return sqlCommand;
        }
    }
}