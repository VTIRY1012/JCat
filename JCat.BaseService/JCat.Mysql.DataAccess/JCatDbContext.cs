using JCat.Mysql.DataAccess.Util;
using Microsoft.EntityFrameworkCore;

namespace JCat.Mysql.DataAccess;
public class JCatDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(ConfigUtil.ConnectionString);
    }
}