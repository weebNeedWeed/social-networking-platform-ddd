using BuildingBlocks.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInsfrastructureBuildingBlocks(this IServiceCollection services)
    {
        services.AddSingleton<IReadDbConnectionRouter, NpgsqlReadDbConnectionRouter>();

        return services;
    }
}
