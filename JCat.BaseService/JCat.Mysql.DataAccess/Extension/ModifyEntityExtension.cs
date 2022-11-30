namespace JCat.Mysql.DataAccess.Extension;
public static class ModifyEntityExtension
{
    public static TProp OriginOrNull<TProp>(this TProp originProp, TProp toProp)
    {
        if (toProp == null)
        {
            return originProp;
        }
        else
        {
            return toProp;
        }
    }
}