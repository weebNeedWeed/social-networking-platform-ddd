using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Common.Errors;

public class UserAccountNotFoundError : DomainError
{
    public UserAccountNotFoundError() : base("Auth.UserAccountNotFound", "User account not found.")
    {
    }
}
