using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Common.Errors;

public class UserAlreadyExistsError : DomainError
{
    public UserAlreadyExistsError() : base("Auth.UserAlreadyExists", "A user with this username or email already exists.")
    {
    }
}
