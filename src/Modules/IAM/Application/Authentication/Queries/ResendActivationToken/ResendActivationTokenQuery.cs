using FluentResults;
using MediatR;

namespace Modules.IAM.Application.Authentication.Queries.ResendActivationToken;

public record ResendActivationTokenQuery(Guid UserId) : IRequest<Result>;