using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Relationship.ValueObjects;

public class RelationshipType : ValueObject
{
    public static RelationshipType Follow => new RelationshipType("Follow");
    public static RelationshipType Block => new RelationshipType("Block");
    public static RelationshipType HideStoryFrom => new RelationshipType("HideStoryFrom");

    public string Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private RelationshipType(string value)
    {
        Value = value;
    }
}