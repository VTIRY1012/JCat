using JCat.BaseService.Extensions.BaseType;

namespace JCat.BaseService.Util
{
    public static class JCatUtil
    {
        public static Dictionary<string, object> ToHeaderDictionary(Dictionary<string, object> keyValues, string key, object value)
        {
            if (keyValues.IsNull())
            {
                return keyValues;
            }

            if (value is DateTime || value is DateTime?)
            {
                keyValues.TryAdd(key, value.ToISO8601());
            }

            keyValues.TryAdd(key, value);
            return keyValues;
        }
    }
}
