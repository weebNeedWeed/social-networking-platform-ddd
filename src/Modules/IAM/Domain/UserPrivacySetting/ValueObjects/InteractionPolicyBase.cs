using System.Runtime.InteropServices.Marshalling;
using BuildingBlocks.Domain;

namespace Modules.IAM.Domain.UserPrivacySetting.ValueObjects;

public abstract class InteractionPolicyBase : ValueObject
{
    public string Value { get; private init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    protected InteractionPolicyBase(string value)
    {
        Value = value;
    }
}