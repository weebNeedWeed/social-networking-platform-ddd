namespace BuildingBlocks.Infrastructure.Services;

public class EmailOptions
{
    public const string SectionName = "Email";

    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Host { get; init; } = null!;
    public int Port { get; init; }
}