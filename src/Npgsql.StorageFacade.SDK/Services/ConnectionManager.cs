using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql.StorageFacade.Sdk.Helpers;
using Npgsql.StorageFacade.Sdk.Options;
using Npgsql.StorageFacade.Sdk.Services.Interfaces;

namespace Npgsql.StorageFacade.Sdk.Services
{
    public class ConnectionManager : IConnectionManager
    {
        public async Task<NpgsqlConnection> TryOpenOrRepairConnectionAsync(
            DbConnectionOptions connectionOptions,
            ILogger logger,
            NpgsqlConnection existingConnection = null,
            CancellationToken cancellationToken = default)
        {
            const string logOpenedConnectionInformationMessage = "Npgsql database connection already open";

            if (string.IsNullOrEmpty(connectionOptions.ConnectionString))
            {
                throw new ArgumentException(nameof(connectionOptions.ConnectionString));
            }
            
            using var cancellationTokenSource = new CancellationTokenSource(30_000);

            if (cancellationToken == CancellationToken.None)
            {
                cancellationToken = cancellationTokenSource.Token;
            }

            if (!(existingConnection is {State: ConnectionState.Open}))
            {
                return await TryOpenOrRepairConnectionInternalAsync(
                    connectionOptions,
                    logger,
                    existingConnection,
                    cancellationToken);
            }

            logger.LogInformation(logOpenedConnectionInformationMessage);
            return existingConnection;
        }

        private static async Task<NpgsqlConnection> TryOpenOrRepairConnectionInternalAsync(
            DbConnectionOptions connectionOptions,
            ILogger logger,
            NpgsqlConnection existingConnection,
            CancellationToken cancellationToken)
        {
            existingConnection = new NpgsqlConnection(connectionOptions.ConnectionString);
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionOptions.ConnectionString);

            logger.LogInformation($"A connection opens for {connectionStringBuilder.Database}");

            try
            {
                await RetryTaskHelper.RetryOnExceptionAsync(
                    connectionOptions.RetryCount,
                    connectionOptions.DelayInSeconds,
                    existingConnection.OpenAsync(cancellationToken),
                    logger);

                return existingConnection;
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message, exception);
                existingConnection?.DisposeAsync();
                throw;
            }
        }
    }
}