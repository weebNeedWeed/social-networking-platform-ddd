using FluentResults;
using Mediator;

namespace Modules.IAM.Application.Authentication.Command.RegisterNewUser;

public record RegisterNewUserCommand(
    string UserName,
    string Email,
    string Password
) : IRequest<Result<RegisterNewUserResult>>;