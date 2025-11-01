using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Common.Errors;

public class AccountAlreadyActivatedError : DomainError
{
    public AccountAlreadyActivatedError() : base("Auth.AccountAlreadyActivated", "The account has already been activated or is not pending verification.")
    {
    }
}
