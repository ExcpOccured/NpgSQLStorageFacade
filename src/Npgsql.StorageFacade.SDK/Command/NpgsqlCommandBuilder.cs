using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql.StorageFacade.Sdk.Models.Arguments;

namespace Npgsql.StorageFacade.Sdk.Command
{
    public class NpgsqlCommandBuilder : INpgsqlCommandBuilder
    {
        public NpgsqlCommand BuildCommand(
            Func<IEnumerable<ICommandArgument>, string> buildQueryDelegate,
            List<ICommandArgument> commandArguments,
            Func<string, bool> validateCommandArgumentsDelegate = null)
        {
            var queryString = buildQueryDelegate.Invoke(commandArguments);
            validateCommandArgumentsDelegate?.Invoke(queryString);

            var sqlCommand = new NpgsqlCommand
            {
                CommandText = queryString
            };

            var delegateTable = new Dictionary<Func<bool>, Action>();

            delegateTable.First(x => x.Key.Invoke());

            foreach (var argument in commandArguments)
            {
                sqlCommand.Parameters.AddWithValue(argument.ArgumentVariableName, argument.ArgumentVariableValue);
            }

            return sqlCommand;
        }
    }
}