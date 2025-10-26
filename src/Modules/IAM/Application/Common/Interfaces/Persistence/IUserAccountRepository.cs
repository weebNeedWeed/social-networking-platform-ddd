using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Application.Common.Interfaces.Persistence;

public interface IUserAccountRepository
{
    Task AddAsync(UserAccount userAccount);

    Task<bool> ExistsByUserNameAsync(string username);

    Task<bool> ExistsByEmailAsync(string email);
}