namespace BuildingBlocks.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

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

        if (obj is not ValueObject valueObject)
        {
            return false;
        }

        return this.GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return this.GetEqualityComponents().Aggregate(1, (curr, next) =>
            curr * 23 + next?.GetHashCode() ?? 0);
    }

    public bool Equals(ValueObject? other)
    {
        return this.Equals((object?)other);
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return left is not null
            && right is not null
            && left.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}