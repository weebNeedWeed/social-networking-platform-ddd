namespace SocialNetworkingPlatform.Modules.IAM.Application.Authentication.Queries.LoginQuery;

public sealed record LoginQueryResult(
    Guid UserId,
    string UserName,
    string Email,
    string Role
);
