using System.Data;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Infrastructure.Persistence;

public class UserAccountRespository : IUserAccountRepository
{
    private IDbTransaction _transaction;

    public UserAccountRespository(IDbTransaction transaction)
    {
        _transaction = transaction;
    }

    public Task AddAsync(UserAccount userAccount)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByUserNameAsync(string username)
    {
        throw new NotImplementedException();
    }
}