namespace Modules.IAM.Domain.UserPrivacySetting.ValueObjects;

public class CommentPolicy : InteractionPolicyBase
{
    public static CommentPolicy Everyone => new CommentPolicy("Everyone");
    public static CommentPolicy Following => new CommentPolicy("Following");
    public static CommentPolicy Followers => new CommentPolicy("Followers");
    public static CommentPolicy FollowingAndFollowers => new CommentPolicy("FollowingAndFollowers");
    public static CommentPolicy Off => new CommentPolicy("Off");

    private CommentPolicy(string value) : base(value)
    {
    }
}