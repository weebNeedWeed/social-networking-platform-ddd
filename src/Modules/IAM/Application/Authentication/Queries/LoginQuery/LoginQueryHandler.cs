using BuildingBlocks.Application.Common.Interfaces;
using Dapper;
using FluentResults;
using MediatR;
using Modules.IAM.Application.Common.Interfaces.Authentication;
using Modules.IAM.Domain.Common.Errors;
using Modules.IAM.Domain.UserAccount.ValueObjects;
using SocialNetworkingPlatform.Modules.IAM.Application.Authentication.Queries.LoginQuery;

namespace Modules.IAM.Application.Authentication.Queries.LoginQuery;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginQueryResult>>
{
    private readonly IReadDbConnectionRouter _readDbConnectionRouter;
    private readonly IPasswordHashingService _passwordHashingService;

    public LoginQueryHandler(IReadDbConnectionRouter readDbConnectionRouter, IPasswordHashingService passwordHashingService)
    {
        _readDbConnectionRouter = readDbConnectionRouter;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<Result<LoginQueryResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        using var conn = await _readDbConnectionRouter.GetConnectionAsync(BuildingBlocks.Application.Common.DatabaseSchema.IAM);

        var sql = """
            SELECT 
                userAccountId,
                username,
                email,
                passwordHash,
                status,
                role
            FROM user_accounts
            WHERE username = @userName OR email = @email
        """;

        var user = await conn.QueryFirstOrDefaultAsync<LoginQueryDto>(sql,
            new { userName = request.EmailOrUserName, email = request.EmailOrUserName });

        if (user is null)
        {
            return Result.Fail(new InvalidCredentialsError());
        }

        var isValidPassword = _passwordHashingService.Verity(request.Password, user.PasswordHash);
        if (!isValidPassword)
        {
            return Result.Fail(new InvalidCredentialsError());
        }

        var status = AccountStatus.FromValue(user.Status);
        if (status == AccountStatus.PendingVerification || status == AccountStatus.Locked)
        {
            return Result.Fail(new InvalidCredentialsError());
        }

        if (status == AccountStatus.Active)
        {
            return Result.Fail(new UserNotOnboardedError());
        }

        var result = new LoginQueryResult(
            user.Id, user.UserName, user.Email, user.Role
        );

        return Result.Ok(result);
    }
}