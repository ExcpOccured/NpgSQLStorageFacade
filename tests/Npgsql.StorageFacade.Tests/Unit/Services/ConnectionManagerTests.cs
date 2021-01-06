using System.Data;
using System.Threading.Tasks;
using Npgsql.StorageFacade.Sdk.Options;
using Npgsql.StorageFacade.Sdk.Services.Interfaces;
using Xunit;

namespace Npgsql.StorageFacade.Tests.Unit.Services
{
    public class ConnectionManagerTests : UnitTestBasics
    {
        private readonly DbConnectionOptions _options;

        public ConnectionManagerTests()
        {
            _options = GetOptions<DbConnectionOptions>();
        }

        [Fact]
        public async Task TryOpenConnectionAsync_Successfully()
        {
            var connectionManager = GetService<IConnectionManager>(typeof(IConnectionManager));

            await using var connection = await connectionManager.TryOpenConnectionAsync(
                _options,
                GetLogger<ConnectionManagerTests>());
            
            Assert.NotNull(connection);
            Assert.Equal(ConnectionState.Open, connection.State);
        }
    }
}