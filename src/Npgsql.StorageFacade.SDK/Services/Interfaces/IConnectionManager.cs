using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade.Sdk.Services.Interfaces
{
    [PublicAPI]
    public interface IConnectionManager
    {
        Task<NpgsqlConnection> TryOpenConnectionAsync(
            StorageFacadeOptions connectionOptions,
            ILogger logger,
            CancellationToken cancellationToken = default);
        
        Task<NpgsqlConnection> TryRepairConnectionAsync(
            StorageFacadeOptions connectionOptions,
            ILogger logger,
            NpgsqlConnection existingConnection,
            CancellationToken cancellationToken = default);
    }
}