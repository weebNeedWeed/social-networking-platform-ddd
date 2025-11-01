using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Common.Errors;

public class UserNotOnboardedError : DomainError
{
    public UserNotOnboardedError() : base("Auth.UserNotOnboarded", "User account is not onboarded.")
    {
    }
}
