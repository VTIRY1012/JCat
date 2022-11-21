using JCat.BaseService.Converter;
using JCat.BaseService.MessageCenter;
using Microsoft.Extensions.Primitives;
using System.ComponentModel;

namespace JCat.BaseService.Extensions.BaseType
{
    public static class StringExtension
    {
        public static bool HasValue(this string? value)
        {
            return false == value.IsNullOrWhiteSpace();
        }

        public static bool IsNullOrWhiteSpace(this string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string IfNullToEmpty(this string value)
        {
            if (value.IsNull())
            {
                return string.Empty;
            }

            return value;
        }

        public static bool IsVersionType(this string value)
        {
            var flag = Version.TryParse(value, out _);
            return flag;
        }

        public static IEnumerable<TModel> ToEnumerable<TModel>(this string value)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return Enumerable.Empty<TModel>();
            }

            return JCatSerializer.Deserialize<IEnumerable<TModel>>(value);
        }

        public static decimal? DecimalOrNull(this string value)
        {
            var flag = decimal.TryParse(value, out decimal toValue);
            return flag.IfFalseReturnNull(toValue);
        }

        public static decimal DecimalOrError(this string? value)
        {
            var flag = decimal.TryParse(value, out decimal toValue);
            if (flag)
            {
                return toValue;
            }

            throw new ArgumentException(Message.JsonDecimelConverterArgError);
        }

        public static bool? BooleanOrNull(this string value)
        {
            var flag = bool.TryParse(value, out bool toValue);
            return flag.IfFalseReturnNull(toValue);
        }

        public static DateTime? DateTimeOrNull(this string value)
        {
            var flag = DateTime.TryParse(value, out DateTime toValue);
            return flag.IfFalseReturnNull(toValue);
        }

        public static string GetDisplayName<T>(this string name)
        {
            var prop = typeof(T).GetProperty(name);
            if (prop.IsNull())
            {
                return name;
            }

            var attrs = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            var attr = attrs.FirstOrDefault();
            var content = attr as DisplayNameAttribute;
            if (content.IsNotNull())
            {
                return content.DisplayName;
            }

            return name;
        }

        #region Private
        private static T? IfFalseReturnNull<T>(this bool flag, T value)
        {
            if (flag)
            {
                return value;
            }

            return default(T?);
        }

        #endregion
    }

    public static class StringValueExtension
    {
        public static bool HasValue(this StringValues value)
        {
            return false == value.IsNullOrEmpty();
        }

        public static bool IsNullOrEmpty(this StringValues value)
        {
            return StringValues.IsNullOrEmpty(value);
        }
    }
}
