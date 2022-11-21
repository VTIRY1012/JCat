using JCat.BaseService.Extensions;

namespace JCat.BaseService.JCatException
{
    public class BadRequestException : SystemException
    {
        public BadRequestException(JErrorResult result) : base(result.ToJsonString()) { }
        public BadRequestException(JErrorResult result, Exception innerException) : base(result.ToJsonString(), innerException) { }
    }
}
