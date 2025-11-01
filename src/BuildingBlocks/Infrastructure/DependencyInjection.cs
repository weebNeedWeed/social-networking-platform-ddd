using System.Reflection;
using BuildingBlocks.Application.Common.Behaviors;
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

        Assembly.GetEntryAssembly()!
            .GetReferencedAssemblies()
            .Where(x => x.FullName.Contains("Application") || x.FullName.Contains("Infrastructure"))
            .Select(x => Assembly.Load(x)).ToArray();

        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName is not null && (x.FullName.Contains("Application") || x.FullName.Contains("Infrastructure")))
            .ToArray();

        var mediatRKey = configuration.GetValue<string>("MediatR:Key");
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblies(loadedAssemblies);
            c.LicenseKey = mediatRKey;
            c.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
