using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationBuildingBlocks(this IServiceCollection services)
    {
        return services;
    } 
}