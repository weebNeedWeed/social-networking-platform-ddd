SET ROLE photolite_iam_role;

CREATE TABLE IF NOT EXISTS photolite_iam_schema.user_accounts (
    user_account_id UUID PRIMARY KEY,
    username VARCHAR(50),
    email VARCHAR(255),
    password_hash VARCHAR(255),
    status VARCHAR(50),
    activation_token JSONB,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    avatar VARCHAR(512),
    bio VARCHAR(255),
    user_privacy_setting JSONB,
    role VARCHAR(50)
);

RESET ROLE;