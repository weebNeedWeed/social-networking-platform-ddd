using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.UserAccount.ValueObjects;

public class AccountStatus : ValueObject
{
    public static AccountStatus PendingVerification => new AccountStatus("PendingVerification");
    public static AccountStatus Active => new AccountStatus("Active");
    public static AccountStatus Onboarded => new AccountStatus("Onboarded");
    public static AccountStatus Locked => new AccountStatus("Locked");

    public string Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private AccountStatus(string value)
    {
        Value = value;
    }
}