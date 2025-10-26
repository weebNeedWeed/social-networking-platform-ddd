using BuildingBlocks.Domain;
using Modules.IAM.Domain.UserAccount.ValueObjects;
using Modules.IAM.Domain.UserPrivacySetting.ValueObjects;

namespace Modules.IAM.Domain.UserPrivacySetting;

public class UserPrivacySetting : AggregateRoot<UserPrivacySettingId>
{
    public UserAccountId UserAccount { get; private set; }
    public ProfileVisibility Visibility { get; private set; }
    public CommentPolicy CommentPolicy { get; private set; }
    public MessageRequestPolicy MessageRequestPolicy { get; private set; }

    public UserPrivacySetting(
        UserPrivacySettingId id,
        UserAccountId userAccount,
        ProfileVisibility visibility,
        CommentPolicy commentPolicy,
        MessageRequestPolicy messageRequestPolicy) : base(id)
    {
        UserAccount = userAccount;
        Visibility = visibility;
        CommentPolicy = commentPolicy;
        MessageRequestPolicy = messageRequestPolicy;
    }

    public static UserPrivacySetting CreateNew(UserAccountId userAccountId)
    {
        return new UserPrivacySetting(
            UserPrivacySettingId.CreateUnique(),
            userAccountId,
            ProfileVisibility.Public,
            CommentPolicy.Everyone,
            MessageRequestPolicy.Everyone
        );
    }

    public static UserPrivacySetting From(
        UserPrivacySettingId id,
        UserAccountId userAccount,
        ProfileVisibility visibility,
        CommentPolicy commentPolicy,
        MessageRequestPolicy messageRequestPolicy)
    {
        return new UserPrivacySetting(
            id,
            userAccount,
            visibility,
            commentPolicy,
            messageRequestPolicy
        );
    }
}