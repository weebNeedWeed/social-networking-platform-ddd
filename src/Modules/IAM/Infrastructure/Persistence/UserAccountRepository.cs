using System.Data;
using BuildingBlocks.Infrastructure.Persistence;
using Dapper;
using Modules.IAM.Application.Common.Interfaces.Persistence;
using Modules.IAM.Domain.UserAccount;
using Modules.IAM.Infrastructure.Common;

namespace Modules.IAM.Infrastructure.Persistence;

public class UserAccountRespository : RepositoryBase, IUserAccountRepository
{
    public UserAccountRespository(IDbTransaction transaction) : base(transaction)
    {
    }

    public async Task AddAsync(UserAccount userAccount)
    {
        var command = """
            DELETE FROM user_accounts WHERE userAccountId = @userAccountId;
        """;
        await Connection.ExecuteAsync(command, new { userAccountId = userAccount.Id.Value });

        command = """
            INSERT INTO user_accounts 
                (userAccountId, username, email, passwordHash, status, activationToken, 
                firstName, lastName, avatar, bio, userPrivacySetting, role)
            VALUES 
                (@userAccountId, @userName, @email, @passwordHash, @status, @activationToken::jsonb,
                @firstName, @lastName, @avatar, @bio, @userPrivacySetting, @role);
        """;
        await Connection.ExecuteAsync(command, userAccount.ToUserAccountDataModel());
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var query = "SELECT COUNT(1) FROM user_accounts WHERE email = @email";
        var count = await Connection.ExecuteScalarAsync<int>(query, new { email = email });

        return count > 0;
    }

    public async Task<bool> ExistsByUserNameAsync(string userName)
    {
        var query = "SELECT COUNT(1) FROM user_accounts WHERE username = @username";
        var count = await Connection.ExecuteScalarAsync<int>(query, new { username = userName });

        return count > 0;
    }

    public async Task<UserAccount?> GetByIdAsync(Guid id)
    {
        var sql = """
            SELECT * FROM user_accounts 
            WHERE userAccountId = @userAccountId
            FOR UPDATE
        """;

        var result = await Connection.QueryFirstOrDefaultAsync<UserAccountDataModel>(sql, new {userAccountId = id});

        var aggregate = result?.ToUserAccount();
        return aggregate;
    }
}