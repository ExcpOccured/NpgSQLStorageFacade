using Npgsql.StorageFacade.Common.Models.Transactions;

namespace Npgsql.StorageFacade.Options
{
    public interface ITransactionOptions
    {
        ITransactionContext TransactionContext { get; }
    }
}