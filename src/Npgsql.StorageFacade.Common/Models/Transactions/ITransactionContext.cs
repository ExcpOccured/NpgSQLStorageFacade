namespace Npgsql.StorageFacade.Common.Models.Transactions
{
    public interface ITransactionContext
    {
        NpgsqlConnection Connection { get; }
    }
}