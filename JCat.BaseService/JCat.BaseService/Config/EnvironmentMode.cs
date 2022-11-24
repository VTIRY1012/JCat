using JCat.BaseService.Const;
using JCat.BaseService.Extensions;
using JCat.BaseService.Extensions.BaseType;
using JCat.BaseService.MessageCenter;

namespace JCat.BaseService.Config
{
    public sealed class EnvironmentMode
    {
        public static JRunTime RunTimeSettings { get; private set; } = new JRunTime();
        public static void Initialize(JRunTime runTime)
        {
            RunTimeSettings = runTime;
            RunTimeSettings._ts = DateTime.UtcNow;
            RunTimeSettings.Version = runTime.Version;
        }

        public static bool HasEnvValue() => (RunTimeSettings?.ASPNETCORE_ENVIRONMENT).HasValue();

        public static string GetCurrentEnvironment =>
            HasEnvValue() ?
            RunTimeSettings.ASPNETCORE_ENVIRONMENT :
            throw new ArgumentNullException(Message.GetEnvironmentError);

        public static string GetCurrentShortEnv =>
            GetCurrentEnvironment.ToLower() switch
            {
                EnvironmentModeConst.Debug => EnvironmentShortNameConst.Debug,
                EnvironmentModeConst.Development => EnvironmentShortNameConst.Development,
                EnvironmentModeConst.Staging => EnvironmentShortNameConst.Staging,
                EnvironmentModeConst.Preproduction => EnvironmentShortNameConst.Preproduction,
                EnvironmentModeConst.Production => EnvironmentShortNameConst.Production,
                _ => throw new ArgumentException(Message.GetEnvironmentShortNameError)
            };

        private static string _version = StringConst.DefaultVersion;
        public static string GetVersion()
        {
            try
            {
                if (_version != StringConst.DefaultVersion)
                {
                    return _version;
                }

                var fileName = VersionConst.FileName;
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                using var sr = new StreamReader(path);
                var text = sr.ReadToEnd();

                text.IsNullOrWhiteSpace().ThrowArgumentNullExceptionIfTrue(Message.VersionReadError);
                text.IsVersionType().ThrowArgumentNullExceptionIfFalse(Message.VersionRuleError);

                _version = text;
                return text;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
