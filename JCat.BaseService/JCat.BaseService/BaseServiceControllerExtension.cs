using JCat.BaseService.Extensions.BaseType;
using JCat.BaseService.MessageCenter;
using System.Net;

namespace JCat.BaseService
{
    public static class BaseServiceControllerExtension
    {
        public static JResult NullReturnNotFound(this JResult result)
        {
            if (result.Data.IsNull())
            {
                return new JResult(HttpStatusCode.NotFound, Message.NotFound, Message.NotFound, null);
            }

            return result;
        }
    }
}
