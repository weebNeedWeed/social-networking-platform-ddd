using BuildingBlocks.Domain;
using Modules.IAM.Domain.Relationship.ValueObjects;
using Modules.IAM.Domain.UserAccount.ValueObjects;

namespace Modules.IAM.Domain.Relationship;

public class Relationship : AggregateRoot<RelationshipId>
{
    public UserAccountId FromUserId { get; private set; }
    public UserAccountId ToUserId { get; private set; }
    public RelationshipType RelationshipType { get; private set; }

    private Relationship(RelationshipId id,
        UserAccountId fromUserId,
        UserAccountId toUserId,
        RelationshipType relationshipType) : base(id)
    {
        FromUserId = fromUserId;
        ToUserId = toUserId;
        RelationshipType = relationshipType;
    }

    public static Relationship CreateNew(
        UserAccountId fromUserId,
        UserAccountId toUserId,
        RelationshipType relationshipType)
    {
        return new Relationship(
            RelationshipId.CreateUnique(),
            fromUserId,
            toUserId,
            relationshipType
        );
    }
}