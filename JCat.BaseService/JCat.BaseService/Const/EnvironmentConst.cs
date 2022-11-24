namespace JCat.BaseService.Const
{
    public sealed class EnvironmentModeConst
    {
        public const string Debug = "debug";
        public const string Development = "development";
        public const string Staging = "staging";
        public const string Preproduction = "preproduction";
        public const string Production = "production";
    }

    public sealed class EnvironmentShortNameConst
    {
        public const string Debug = "debug";
        public const string Development = "dev";
        public const string Staging = "stage";
        public const string Preproduction = "preprod";
        public const string Production = "prod";
    }
    public sealed class EnvironmentVariablesConst
    {
        public const string ModeKey = "ASPNETCORE_ENVIRONMENT";
        public const string ApplicationKey = "ApplicationKey";
        public const string ApplicationName = "ApplicationName";
    }

    public sealed class VersionConst
    {
        public const string FileName = ".version";
    }
}
