namespace JCat.BaseService.Extensions.BaseType
{
    public static class GenericsExtension
    {
        public static bool IsNull<T>(this T value)
        {
            if (value == null)
            {
                return true;
            }

            return false;
        }

        public static bool IsNotNull<T>(this T value)
        {
            return !value.IsNull();
        }
    }
}
