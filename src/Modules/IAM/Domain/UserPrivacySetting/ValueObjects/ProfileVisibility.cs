using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.UserPrivacySetting.ValueObjects;

public class ProfileVisibility : ValueObject
{
    public static ProfileVisibility Public => new ProfileVisibility("Public");
    public static ProfileVisibility Private => new ProfileVisibility("Private");

    public string Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private ProfileVisibility(string value)
    {
        Value = value;
    }

    public static ProfileVisibility From(string value)
    {
        return value switch
        {
            "Public" => Public,
            "Private" => Private,
            _ => Public,
        };
    }

    public bool IsPublic() => this.Equals(Public);
    public bool IsPrivate() => this.Equals(Private);
}
