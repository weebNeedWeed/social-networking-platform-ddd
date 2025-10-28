CREATE ROLE photolite_iam_role
LOGIN
PASSWORD 'photolite_iam_role';

CREATE SCHEMA photolite_iam_schema
AUTHORIZATION photolite_iam_role;

ALTER ROLE photolite_iam_role SET search_path TO photolite_iam_schema;