using System.Data;

namespace BuildingBlocks.Application.Common.Interfaces;

public interface IReadDbConnectionRouter
{
    Task<IDbConnection> GetConnectionAsync(DatabaseSchema schema);
}