using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade.Sdk.Services.Interfaces
{
    public interface IConnectionManager
    {
        Task<NpgsqlConnection> TryOpenOrRepairConnectionAsync(
            DbConnectionOptions connectionOptions,
            ILogger logger,
            NpgsqlConnection existingConnection = null,
            CancellationToken cancellationToken = default);
    }
}