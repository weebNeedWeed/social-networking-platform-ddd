using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.Relationship.ValueObjects;

public class RelationshipId : ValueObject
{
    public Guid Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private RelationshipId(Guid value)
    {
        Value = value;
    }

    public static RelationshipId CreateUnique() => new RelationshipId(Guid.NewGuid());
}