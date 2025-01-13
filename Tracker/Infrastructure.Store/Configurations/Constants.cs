namespace Infrastructure.Store.Configurations;
internal static class Constants
{
    public const string Schema = "tracker";

    public static class Tables
    {
        public const string Roles = "roles";
        public const string RoleClaims = "roleclaims";
        public const string Users = "users";
        public const string UserClaims = "userclaims";
        public const string UserLogins = "userlogins";
        public const string UserRoles = "userroles";
        public const string UserTokens = "usertokens";
    }

    public const int TextShort = 128;
    public const int TextMedium = 256;
    public const int TextLong = 450;
}
