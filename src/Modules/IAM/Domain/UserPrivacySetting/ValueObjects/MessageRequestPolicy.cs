namespace Modules.IAM.Domain.UserPrivacySetting.ValueObjects;

public class MessageRequestPolicy : InteractionPolicyBase
{
    public static MessageRequestPolicy Everyone => new MessageRequestPolicy("Everyone");
    public static MessageRequestPolicy Followers => new MessageRequestPolicy("Followers");
    public static MessageRequestPolicy NoOne => new MessageRequestPolicy("NoOne");

    private MessageRequestPolicy(string value) : base(value)
    {
    }
}