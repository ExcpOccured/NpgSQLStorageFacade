using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Npgsql.StorageFacade.Sdk.Options;

namespace Npgsql.StorageFacade.Sdk.Connection
{
    [PublicAPI]
    public interface IConnectionManager
    {
        Task<NpgsqlConnection> TryOpenConnectionAsync(
            ILogger? logger = null,
            CancellationToken cancellationToken = default);
        
        
        Task<NpgsqlConnection> TryRepairConnectionAsync(
            NpgsqlConnection existingConnection,
            ILogger? logger = null,
            CancellationToken cancellationToken = default);
    }
}