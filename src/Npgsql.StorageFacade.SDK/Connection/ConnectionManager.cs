using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql.StorageFacade.Sdk.Helpers;
using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade.Sdk.Connection
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly StorageFacadeOptions _options;

        public ConnectionManager(IOptions<StorageFacadeOptions> options)
        {
            _options = options.Value;
        }

        public async Task<NpgsqlConnection> TryOpenConnectionAsync(
            ILogger? logger = null,
            CancellationToken cancellationToken = default)
        {
            using var cancellationTokenSource = new CancellationTokenSource(_options.DelayInMilliseconds);

            if (cancellationToken == default)
            {
                cancellationToken = cancellationTokenSource.Token;
            }
            
            return await TryOpenConnectionInternalAsync(
                _options,
                logger,
                cancellationToken);
        }

        public async Task<NpgsqlConnection> TryRepairConnectionAsync(
            NpgsqlConnection existingConnection, 
            ILogger? logger = null,
            CancellationToken cancellationToken = default)
        {
            const string logOpenedConnectionInformationMessage = "Npgsql database connection already open";
            
            using var cancellationTokenSource = new CancellationTokenSource();

            if (cancellationToken == default)
            {
                cancellationToken = cancellationTokenSource.Token;
            }

            if (existingConnection is not {State: ConnectionState.Open})
            {
                return await TryOpenConnectionInternalAsync(
                    _options,
                    logger,
                    cancellationToken,
                    existingConnection);
            }

            logger?.LogInformation(logOpenedConnectionInformationMessage);
            return existingConnection;
        }

        private static async Task<NpgsqlConnection> TryOpenConnectionInternalAsync(
            StorageFacadeOptions connectionOptions,
            ILogger? logger,
            CancellationToken cancellationToken,
            NpgsqlConnection? existingConnection = null)
        {
            existingConnection = new NpgsqlConnection(connectionOptions.ConnectionString);
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionOptions.ConnectionString);

            logger.LogInformation($"A connection opens for {connectionStringBuilder.Database}");

            try
            {
                await RetryTaskHelper.RetryOnExceptionAsync(
                    connectionOptions.RetryCount,
                    connectionOptions.DelayInMilliseconds,
                    existingConnection.OpenAsync(cancellationToken),
                    logger);

                return existingConnection;
            }
            catch (Exception exception)
            {
                logger.LogError(exception?.Message, exception);
                existingConnection?.DisposeAsync();
                throw;
            }
        }
    }
}