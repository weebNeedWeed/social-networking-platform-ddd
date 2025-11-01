using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Application.Common.Interfaces.Persistence;

public interface IUserAccountRepository
{
    Task AddAsync(UserAccount userAccount);

    Task<UserAccount?> GetByIdAsync(Guid id);

    Task<bool> ExistsByUserNameAsync(string userName);

    Task<bool> ExistsByEmailAsync(string email);
}