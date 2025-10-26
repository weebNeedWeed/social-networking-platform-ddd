using System.Data;

namespace BuildingBlocks.Infrastructure.Persistence;

public interface IReadDbConnectionRouter
{
    Task<IDbConnection> GetConnectionAsync(DatabaseSchema schema);
}