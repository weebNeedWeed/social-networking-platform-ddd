using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.UserAccount.ValueObjects;

public class UserAccountId : ValueObject
{
    public Guid Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private UserAccountId(Guid value)
    {
        Value = value;
    }

    public static UserAccountId CreateUnique()
    {
        return new UserAccountId(Guid.NewGuid());
    }
}