using JCat.BaseService.Const;

namespace JCat.BaseService.Extensions.BaseType
{
    public static class DateTimeExtension
    {
        public static string ToISO8601(this DateTime dateTime)
        {
            return dateTime.ToString(StringConst.BaseDateTimeString);
        }

        public static string ToISO8601(this DateTime? dateTime)
        {
            return GetSO8601(dateTime);
        }

        public static string ToISO8601(this object dateTime)
        {
            var result = dateTime as DateTime?;
            return GetSO8601(result);
        }

        private static string GetSO8601(DateTime? dateTime)
        {
            return
                dateTime.HasValue ?
                dateTime.Value.ToString(StringConst.BaseDateTimeString) :
                StringConst.Empty;
        }
    }
}
