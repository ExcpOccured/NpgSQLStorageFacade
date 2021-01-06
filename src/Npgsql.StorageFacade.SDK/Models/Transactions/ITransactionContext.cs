namespace Npgsql.StorageFacade.Sdk.Models.Transactions
{
    public interface ITransactionContext
    {
        NpgsqlConnection Connection { get; }
    }
}