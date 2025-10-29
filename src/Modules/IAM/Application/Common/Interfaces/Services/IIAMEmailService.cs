namespace Modules.IAM.Application.Common.Interfaces.Services;

public interface IIAMEmailService
{
    Task SendActivationEmailAsync(string email, string activationToken);
}
