using JCat.Mysql.DataAccess.Enum;

namespace JCat.Mysql.DataAccess.Model;
public class BaseEntity
{
    public string Status { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;
    public DateTime Create_At { get; set; } = DateTime.UtcNow;
    public string Modifier { get; set; } = string.Empty;
    public DateTime Modify_At { get; set; } = DateTime.UtcNow;
}