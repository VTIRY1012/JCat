namespace JCat.Mysql.DataAccess.Util;
public class ConfigUtil
{
    public static void InitializeMysql(string connectionString)
    {
        _connectionString = connectionString;
    }

    private static string _connectionString;
    public static string ConnectionString { get { return _connectionString; } }
}

