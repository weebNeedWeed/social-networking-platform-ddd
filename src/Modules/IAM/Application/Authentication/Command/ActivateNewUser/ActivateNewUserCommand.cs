using FluentResults;
using MediatR;

namespace Modules.IAM.Application.Authentication.Command.ActivateNewUser;
        
public record ActivateNewUserCommand(Guid UserId, string Token) : IRequest<Result>;
