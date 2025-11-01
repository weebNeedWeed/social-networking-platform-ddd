using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.UserAccount.ValueObjects;

public class ActivationToken : ValueObject
{
    public string Token { get; private init; }

    public DateTime ExpiresAt { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Token;
        yield return ExpiresAt;
    }

    private ActivationToken(string token, DateTime expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }

    public static ActivationToken CreateUnique(TimeSpan validityPeriod)
    {
        var token = Guid.NewGuid().ToString("N");
        var expiresAt = DateTime.UtcNow.Add(validityPeriod);
        return new ActivationToken(token, expiresAt);
    }

    public static ActivationToken From(string token, DateTime expiresAt)
    {
        return new ActivationToken(token, expiresAt);
    }
}