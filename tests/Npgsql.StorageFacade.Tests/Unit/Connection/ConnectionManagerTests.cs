using System.Data;
using System.Threading.Tasks;
using Npgsql.StorageFacade.Sdk.Connection;
using Xunit;

namespace Npgsql.StorageFacade.Tests.Unit.Connection
{
    public class ConnectionManagerTests : UnitTestBasics
    {
        [Fact]
        public async Task TryOpenConnectionAsync_Successfully()
        {
            var connectionManager = GetService<IConnectionManager>(typeof(IConnectionManager));

            await using var connection = await connectionManager.TryOpenConnectionAsync(
                GetLogger<ConnectionManagerTests>());
            
            Assert.NotNull(connection);
            Assert.Equal(ConnectionState.Open, connection.State);
        }
    }
}