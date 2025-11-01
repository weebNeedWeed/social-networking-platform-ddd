using BuildingBlocks.Domain;
using FluentResults;

namespace Modules.IAM.Domain.Common.Errors;

public class InvalidCredentialsError : DomainError
{
    public InvalidCredentialsError() 
        : base("Auth.InvalidCredentials", "Invalid username or password.")
    {
    }
}
