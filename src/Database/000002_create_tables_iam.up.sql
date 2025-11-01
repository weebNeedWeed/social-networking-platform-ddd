SET ROLE photolite_iam_role;

CREATE TABLE IF NOT EXISTS photolite_iam_schema.user_accounts (
    userAccountId UUID PRIMARY KEY,
    username VARCHAR(50),
    email VARCHAR(255),
    passwordHash VARCHAR(255),
    status VARCHAR(50),
    activationToken JSONB,
    firstName VARCHAR(50),
    lastName VARCHAR(50),
    avatar VARCHAR(512),
    bio VARCHAR(255),
    userPrivacySetting UUID,
    role VARCHAR(50)
);

RESET ROLE;