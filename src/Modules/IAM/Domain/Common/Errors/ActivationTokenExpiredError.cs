using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Common.Errors;

public class ActivationTokenExpiredError : DomainError
{
    public ActivationTokenExpiredError() : base("Auth.ActivationTokenExpired", "The activation token has expired.")
    {
    }
}
