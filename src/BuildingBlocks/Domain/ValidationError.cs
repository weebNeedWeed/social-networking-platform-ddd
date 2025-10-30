namespace BuildingBlocks.Domain;

public class ValidationError : DomainError
{
    public ValidationError(string code, string message) : base(code, message)
    {
    }
}