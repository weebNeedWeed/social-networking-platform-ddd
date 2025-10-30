using FluentResults;

namespace BuildingBlocks.Domain;

public abstract class DomainError : Error
{
    public string Code { get; init; }

    public DomainError(string code, string message) : base(message)
    {
        Code = code;
        Metadata.Add("Code", code);
    }
}