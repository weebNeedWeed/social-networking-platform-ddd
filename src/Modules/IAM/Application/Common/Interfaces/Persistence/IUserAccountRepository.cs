using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Application.Common.Interfaces.Persistence;

public interface IUserAccountRepository
{
    Task AddAsync(UserAccount userAccount);

    Task<bool> ExistsByUserNameAsync(string userName);

    Task<bool> ExistsByEmailAsync(string email);
}