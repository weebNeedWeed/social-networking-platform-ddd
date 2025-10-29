using System.Data;
using BuildingBlocks.Application.Common;
using BuildingBlocks.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace BuildingBlocks.Infrastructure.Persistence;

public class NpgsqlReadDbConnectionRouter : IReadDbConnectionRouter
{
    private readonly IConfiguration _configuration;

    public NpgsqlReadDbConnectionRouter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IDbConnection> GetConnectionAsync(DatabaseSchema schema)
    {
        var conn = new NpgsqlConnection(GetConnectionString(schema));
        await conn.OpenAsync();
        return conn;
    }

    private string GetConnectionString(DatabaseSchema schema) => schema switch
    {
        DatabaseSchema.IAM => _configuration.GetConnectionString("IAM")!,
        _ => throw new NotImplementedException()
    };
}