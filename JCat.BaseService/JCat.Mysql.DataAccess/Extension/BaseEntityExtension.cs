using JCat.Mysql.DataAccess.Enum;
using JCat.Mysql.DataAccess.Model;

namespace JCat.Mysql.DataAccess.Extension;
public static class BaseEntityExtension
{
    public static TEntity AddCreateInfo<TEntity>(this TEntity entity, string userId) where TEntity : BaseEntity
    {
        entity.Creator = userId;
        entity.Create_At = DateTime.UtcNow;
        entity.Modifier = userId;
        entity.Modify_At = DateTime.UtcNow;
        return entity;
    }

    public static TEntity AddModifyInfo<TEntity>(this TEntity entity, string userId) where TEntity : BaseEntity
    {
        entity.Modifier = userId;
        entity.Modify_At = DateTime.UtcNow;
        return entity;
    }

    public static TEntity AddNormalStatus<TEntity>(this TEntity entity) where TEntity : BaseEntity
    {
        entity.Status = TableStatusEnum.Normal.ToString();
        return entity;
    }
}
