using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.UserPrivacySetting.ValueObjects;

public class UserPrivacySettingId : ValueObject
{
    public Guid Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value; 
    }

    private UserPrivacySettingId(Guid value)
    {
        Value = value;
    }

    public static UserPrivacySettingId CreateUnique()
    {
        return new UserPrivacySettingId(Guid.NewGuid());
    }
}