using BuildingBlocks.Application.Common.Interfaces;
using BuildingBlocks.Infrastructure.Persistence;
using BuildingBlocks.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInsfrastructureBuildingBlocks(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.SectionName));
        services.AddSingleton<IReadDbConnectionRouter, NpgsqlReadDbConnectionRouter>();
        services.AddSingleton<IEmailServiceBase, EmailServiceBase>();

        return services;
    }
}
