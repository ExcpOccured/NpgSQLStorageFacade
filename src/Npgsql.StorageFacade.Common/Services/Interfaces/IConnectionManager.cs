using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Npgsql.StorageFacade.Common.Services.Interfaces
{
    public interface IConnectionManager
    {
        Task<NpgsqlConnection> TryOpenOrRepairConnectionAsync(
            string connectionString,
            ILogger logger,
            NpgsqlConnection existingConnection = null,
            CancellationToken cancellationToken = default);
    }
}