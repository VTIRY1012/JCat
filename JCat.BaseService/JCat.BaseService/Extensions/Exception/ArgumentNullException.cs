namespace JCat.BaseService.Extensions
{
    public static class ArgumentNull
    {
        public static void ThrowArgumentNullException(this string message)
        {
            throw new ArgumentNullException(message);
        }

        public static void ThrowArgumentNullExceptionIfTrue(this bool condition, string message)
        {
            if (condition)
            {
                message.ThrowArgumentNullException();
            }
        }

        public static void ThrowArgumentNullExceptionIfFalse(this bool condition, string message)
        {
            if (!condition)
            {
                message.ThrowArgumentNullException();
            }
        }
    }
}
