using FluentResults;
using MediatR;
using Modules.IAM.Domain.UserAccount;
using SocialNetworkingPlatform.Modules.IAM.Application.Authentication.Queries.LoginQuery;

namespace Modules.IAM.Application.Authentication.Queries.LoginQuery;

public record LoginQuery(
    string EmailOrUserName,
    string Password
) : IRequest<Result<LoginQueryResult>>;