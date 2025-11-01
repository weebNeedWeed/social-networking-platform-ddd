using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Common.Errors;

public class InvalidActivationTokenError : DomainError
{
    public InvalidActivationTokenError() : base("Auth.InvalidActivationToken", "The provided activation token is invalid.")
    {
    }
}
