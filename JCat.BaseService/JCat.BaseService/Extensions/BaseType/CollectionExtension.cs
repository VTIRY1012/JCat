namespace JCat.BaseService.Extensions.BaseType
{
    public static class CollectionExtension
    {
        public static bool EmptyOrNull<TSource>(this IEnumerable<TSource> list)
        {
            var result = (list == null || !list.Any());
            return result;
        }

        public static bool EmptyOrNull<TKey, TValue>(this Dictionary<TKey, TValue> list)
        {
            var result = (list == null || !list.Any());
            return result;
        }

        public static IDictionary<string, object> ToDictionary(this (string key, object value) source)
        {
            var dictionary = new Dictionary<string, object>()
            {
                {source.key, source.value}
            };

            return dictionary;
        }

        public static IEnumerable<List<T>> Partition<T>(this List<T> list, int size)
        {
            int total = list.Count;
            int groupCount = (total / size) + (total % size > 0 ? 1 : 0);

            for (int i = 0; i < groupCount; i++)
            {
                yield return new List<T>(list.Skip(size * i).Take(size));
            }
        }
    }
}
