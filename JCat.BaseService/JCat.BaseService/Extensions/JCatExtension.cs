using JCat.BaseService.Util;

namespace JCat.BaseService.Extensions
{
    public static class JCatHeaderExtension
    {
        public static Dictionary<string, object> ToJCatDictionary(this Dictionary<string, object> keyValues, string name, object value)
        {
            return JCatUtil.ToHeaderDictionary(keyValues, name, value);
        }
    }
}
