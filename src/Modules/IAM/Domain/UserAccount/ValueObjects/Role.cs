using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.UserAccount.ValueObjects;

public class Role : ValueObject
{
    public static Role Administrator => new Role("Administrator");
    public static Role Moderator => new Role("Moderator");
    public static Role User => new Role("User");

    public string Name { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
    }

    private Role(string name)
    {
        Name = name;
    }
}