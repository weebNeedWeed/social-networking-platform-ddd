using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.IAM.Application;
using Modules.IAM.Application.Common.Interfaces.Authentication;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Modules.IAM.Application.Common.Interfaces.Services;
using Modules.IAM.Infrastructure.Authentication;
using Modules.IAM.Infrastructure.Persistence;
using Modules.IAM.Infrastructure.Services;

namespace Modules.IAM.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterIAMModule(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHashingService, PasswordHasingService>();
        services.AddScoped<IIAMUnitOfWork, IAMUnitOfWork>();
        services.AddScoped<IIAMEmailService, IAMEmailService>();
        services.AddValidatorsFromAssemblyContaining<IAMApplicationMarker>();
        return services;
    }
}
