using JCat.BaseService.Const;

namespace JCat.BaseService
{
    public sealed class JRunTime
    {
        public DateTime _ts { get; set; }
        public string ASPNETCORE_ENVIRONMENT { get; set; } = StringConst.Empty;
        public string Version { get; set; } = StringConst.DefaultVersion;
        public string MachineName { get; set; } = StringConst.Empty;
        public JApplication Application { get; set; } = new JApplication();
    }

    public sealed class JApplication
    {
        public string ApplicationKey { get; set; } = StringConst.Empty;
        public string ApplicationName { get; set; } = StringConst.Empty;
    }

    public sealed class JServer
    {
        public string ConfigUrl { get; set; } = StringConst.Empty;
        public string RedisUrl { get; set; } = StringConst.Empty;
    }
}
