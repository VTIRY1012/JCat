using JCat.BaseService.Attribute;
using JCat.BaseService.MessageCenter;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace JCat.BaseService.Extensions.BaseType
{
    public static class TypeEnumExtension
    {
        [return: MaybeNull]
        public static string GetEnumName<T>(this T enumValue) where T : System.Enum
        {
            var name = System.Enum.GetName(typeof(T), enumValue);
            name.IsNull().ThrowArgumentNullExceptionIfTrue(Message.GetEnumNameError);
            return name;
        }

        public static T ToEnum<T>(this string name) where T : System.Enum
        {
            var typeName = (T)System.Enum.Parse(typeof(T), name);
            return typeName;
        }

        public static bool EqualToEnum<T>(this string name, T compareEnum) where T : System.Enum
        {
            var originEnum = name.ToEnum<T>();
            return Equals(originEnum, compareEnum);
        }

        public static IEnumerable<TEnum> FilterIgnoreEnums<TEnum>(this IEnumerable<TEnum> source) where TEnum : System.Enum
        {
            var enumNames = source.Select(x => x.ToString());

            return typeof(TEnum).GetFields()
                .Where(x => enumNames.Contains(x.Name))
                .Select(x => new
                {
                    Name = x.Name,
                    Ignore = x.GetCustomAttribute(typeof(EnumIgnoreAttribute)) == null
                })
                .Where(x => x.Ignore == true)
                .Select(x => Enum.Parse(typeof(TEnum), x.Name))
                .Cast<TEnum>();
        }
    }
}
