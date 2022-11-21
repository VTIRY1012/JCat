using JCat.BaseService.Converter;
using JCat.BaseService.Extensions.BaseType;

namespace JCat.BaseService.Extensions
{
    public static class ExceptionExtension
    {
        public static string ToJsonString(this JErrorResult errResult)
        {
            var result = JCatSerializer.Serialize(errResult);
            return result;
        }

        public static JErrorResult GetResponseContent(this Exception exception)
        {
            if (exception.IsNull())
            {
                return new JErrorResult();
            }

            return new JErrorResult()
            {
                Message = exception.Message,
                ErrorMessage = exception.StackTrace
            };
        }
    }
}
