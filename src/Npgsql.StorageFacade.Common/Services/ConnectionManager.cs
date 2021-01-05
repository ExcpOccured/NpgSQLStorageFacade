using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql.StorageFacade.Common.Helpers;
using Npgsql.StorageFacade.Common.Services.Interfaces;

namespace Npgsql.StorageFacade.Common.Services
{
    public class ConnectionManager : IConnectionManager
    {
        public async Task<NpgsqlConnection> TryOpenOrRepairConnectionAsync(
            string connectionString,
            ILogger logger,
            NpgsqlConnection existingConnection = null,
            CancellationToken cancellationToken = default)
        {
            const string logOpenedConnectionInformationMessage = "Npgsql database connection already open";

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }

            if (!(existingConnection?.State is ConnectionState.Open))
                return await TryOpenOrRepairConnectionInternalAsync(
                    connectionString,
                    logger,
                    existingConnection,
                    cancellationToken);

            logger.LogInformation(logOpenedConnectionInformationMessage);
            return existingConnection;
        }

        private async Task<NpgsqlConnection> TryOpenOrRepairConnectionInternalAsync(
            string connectionString,
            ILogger logger,
            NpgsqlConnection existingConnection,
            CancellationToken cancellationToken = default)
        {
            existingConnection = new NpgsqlConnection(connectionString);
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

            logger.LogInformation($"A connection opens for {connectionStringBuilder.Database}");

            using var cancellationTokenSource = new CancellationTokenSource(30_000);

            cancellationToken = cancellationToken == CancellationToken.None
                ? cancellationTokenSource.Token
                : cancellationToken;

            try
            {
                await RetryTaskHelper.RetryOnExceptionAsync(
                    3,
                    5,
                    () => existingConnection.OpenAsync(cancellationToken),
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