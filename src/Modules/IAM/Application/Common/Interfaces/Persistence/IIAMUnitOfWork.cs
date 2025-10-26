namespace Modules.IAM.Application.Common.Interfaces.Persistence;

public interface IIAMUnitOfWork
{
    IUserAccountRepository UserAccountRepository { get; }

    Task CommitAsync();
}