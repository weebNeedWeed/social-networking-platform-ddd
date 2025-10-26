namespace BuildingBlocks.Domain;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId: notnull
{
    public TId Id { get; private set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (!obj.GetType().Equals(this.GetType()))
        {
            return false;
        }

        if (obj is not Entity<TId> entity)
        {
            return false;
        }

        return Id.Equals(entity.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(ValueObject? other)
    {
        return this.Equals((object?)other);
    }

    public bool Equals(Entity<TId>? other)
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return left is not null
            && right is not null
            && left.Equals(right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }
}