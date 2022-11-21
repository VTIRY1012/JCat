using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JCat.BaseService
{
    public class BaseServiceController : ControllerBase
    {
        // 200
        protected JResult Successed(object data)
        {
            return new JResult(HttpStatusCode.OK, data);
        }

        // 201
        protected JResult SuccessedCreate(object data)
        {
            return new JResult(HttpStatusCode.Created, data);
        }

        // 400
        protected JResult Error(string message, string errorMessage)
        {
            return new JResult(HttpStatusCode.BadRequest, message, errorMessage, null);
        }

        protected JResult Error(string message, string errorMessage, object data)
        {
            return new JResult(HttpStatusCode.BadRequest, message, errorMessage, data);
        }

        // Other
        protected JResult Result(HttpStatusCode code, string message, string errorMessage, object data)
        {
            return new JResult(code, message, errorMessage, data);
        }

        // File
        // todo: to be coming.
    }
}
