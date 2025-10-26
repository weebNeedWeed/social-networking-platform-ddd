using Modules.IAM.Application.Common.Interfaces.Persistence;

namespace Modules.IAM.Infrastructure.Persistence;

public class IAMUnitOfWork : IIAMUnitOfWork
{
    public IUserAccountRepository UserAccountRepository => throw new NotImplementedException();

    public Task CommitAsync()
    {
        throw new NotImplementedException();
    }
}