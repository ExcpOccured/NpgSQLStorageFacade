using System;
using System.Collections.Generic;
using System.Data.Common;
using JetBrains.Annotations;
using Npgsql.StorageFacade.Sdk.Models.Arguments;
using NpgsqlTypes;

namespace Npgsql.StorageFacade.Sdk.Extensions
{
    [PublicAPI]
    public static class NpgsqlParameterCollectionExtensions
    {
        public static NpgsqlParameter AddWithValue(
            this DbParameterCollection parameterCollection, 
            string name, 
            object value)
        {
            var parameter = new NpgsqlParameter(name, value);
            parameterCollection.Add(parameter);
            return parameter;
        }

        public static void AddRangeWithArguments(
            this DbParameterCollection parameterCollection,
            List<ICommandArgument> commandArguments)
        {
            foreach (var argument in commandArguments)
            {
                parameterCollection.AddWithValue(argument.ArgumentVariableName, argument.ArgumentVariableValue);
            }
        }

        public static NpgsqlParameter AddWithNullableValue(
            this DbParameterCollection parameterCollection, 
            string name,
            object? value)
        {
            return parameterCollection.AddWithValue(name, value ?? DBNull.Value);
        }

        public static NpgsqlParameter AddInt64Array(
            this DbParameterCollection parameterCollection,
            string name,
            long[] value)
        {
            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            var parameter = new NpgsqlParameter(name, value)
            {
                NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Bigint
            };
            
            parameterCollection.Add(parameter);
            return parameter;
        }
    }
}