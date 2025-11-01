using System.Data;
using BuildingBlocks.Application.Common.Interfaces;
using Dapper;
using FluentResults;
using MediatR;
using Modules.IAM.Application.Common.Interfaces.Services;

namespace Modules.IAM.Application.Authentication.Queries.ResendActivationToken;

public class ResendActivationTokenQueryHandler : IRequestHandler<ResendActivationTokenQuery, Result>
{
    private readonly IReadDbConnectionRouter _readDbConnectionRouter;
    private readonly IIAMEmailService _iAMEmailService;

    public ResendActivationTokenQueryHandler(IReadDbConnectionRouter readDbConnectionRouter, IIAMEmailService iAMEmailService)
    {
        _readDbConnectionRouter = readDbConnectionRouter;
        _iAMEmailService = iAMEmailService;
    }

    public async Task<Result> Handle(ResendActivationTokenQuery request, CancellationToken cancellationToken)
    {
        var conn = await _readDbConnectionRouter.GetConnectionAsync(BuildingBlocks.Application.Common.DatabaseSchema.IAM);
        var sql = """
            SELECT 
                email, 
                userName,
                activation_token ->> 'Token' token,
                activation_token ->> 'ExpiresAt' expiresAt
            FROM user_accounts 
            WHERE user_account_id = @userId AND activation_token IS NOT NULL;
        """;

        var row = await conn.QueryFirstOrDefaultAsync<ResendActivationTokenDto>(sql, new {userId = request.UserId});
        conn.Close();
        if (row is null)
        {
            return Result.Fail("There is no user account with given userId exists");
        }

        if (DateTime.UtcNow >= row.ExpiresAt)
        {
            return Result.Fail("Activation token is expired");
        }

        await _iAMEmailService.SendActivationEmailAsync(row.Username, row.Email, row.Token);

        return Result.Ok();
    }
}