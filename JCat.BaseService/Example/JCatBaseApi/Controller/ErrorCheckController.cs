using JCat.BaseService;
using JCat.BaseService.JCatException;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Authentication;

namespace JCatBaseApi.Controller
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ErrorCheckController : BaseServiceController
    {
        public async Task<JResult> ThrowBadRequest()
        {
            var result = new JErrorResult()
            {
                Message = "Frontend Message",
                ErrorMessage = "Backend Message",
                Data = Guid.NewGuid()
            };
            await Task.CompletedTask;
            throw new BadRequestException(result);
        }

        public async Task<JResult> ThrowAuthentication()
        {
            var result = "Authentication Error";
            await Task.CompletedTask;
            throw new AuthenticationException(result);
        }

        public async Task<JResult> ThrowAuthorization()
        {
            var result = "Authorization Error";
            await Task.CompletedTask;
            throw new AuthorizationException(result);
        }

        public async Task<JResult> ThrowServerError()
        {
            var result = "ServerError";
            await Task.CompletedTask;
            throw new Exception(result);
        }

        public async Task<JResult> ErrorResult()
        {
            await Task.CompletedTask;
            return Error("Frontend Message", "Backend Message");
        }

        public async Task<JResult> StatusCodeError()
        {
            var result = "Other Error";
            await Task.CompletedTask;
            return Result(HttpStatusCode.Gone, result, result, null);
        }
    }
}
