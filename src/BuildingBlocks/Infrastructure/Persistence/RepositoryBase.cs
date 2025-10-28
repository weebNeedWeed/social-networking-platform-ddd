using System.Data;

namespace BuildingBlocks.Infrastructure.Persistence;

public abstract class RepositoryBase
{
    protected IDbTransaction Transaction;
    protected IDbConnection Connection => Transaction.Connection!;

    protected RepositoryBase(IDbTransaction dbTransaction)
    {
        Transaction = dbTransaction;
    }
}