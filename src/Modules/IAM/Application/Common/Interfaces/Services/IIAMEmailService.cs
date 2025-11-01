using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Application.Common.Interfaces.Services;

public interface IIAMEmailService
{
    Task SendActivationEmailAsync(string userName, string email, string token);
}
