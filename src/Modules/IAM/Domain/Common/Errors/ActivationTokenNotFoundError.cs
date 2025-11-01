using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Common.Errors;

public class ActivationTokenNotFoundError : DomainError
{
    public ActivationTokenNotFoundError() : base("Auth.ActivationTokenNotFound", "Activation token not found for this account.")
    {
    }
}
