using JCat.BaseService.Extensions;
using JCat.BaseService.Extensions.Service;
using JCat.BaseService.JCatException;
using JCat.BaseService.Util;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Authentication;

namespace JCat.BaseService.Middleware
{
    public class JBaseMiddleware
    {
        private readonly RequestDelegate _next;

        public JBaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // cors headers
            await JCatMiddlewareUtil.SetCors(context);

            // option method return 204
            if (context.IsHeaderMethodOptions())
            {
                await Task.CompletedTask;
                return;
            }

            // response data
            var result = new object();
            // event id
            string processId = Guid.NewGuid().ToString();
            // orgin data
            Stream originalRespBodyStream = context.Response.Body;
            try
            {
                context.SetJRequestBaseHeaders(processId);

                using var ms = new MemoryStream();
                context.Response.Body = ms;
                // do request.
                await _next(context);

                context.SetJResponseBaseHeaders();

                // enter response
                result = await context.GetResponseBody(ms);
            }
            catch (BadRequestException badRequestEx)
            {
                await context.SetJErrorResponseBaseHeaders(HttpStatusCode.BadRequest, processId);
                result = badRequestEx.GetResponseContent();
            }
            catch (AuthenticationException authenticationEx)
            {
                await context.SetJErrorResponseBaseHeaders(HttpStatusCode.Unauthorized, processId);
                result = authenticationEx.GetResponseContent();
            }
            catch (AuthorizationException authorizationEx)
            {
                await context.SetJErrorResponseBaseHeaders(HttpStatusCode.Forbidden, processId);
                result = authorizationEx.GetResponseContent();
            }
            catch (Exception ex)
            {
                await context.SetJErrorResponseBaseHeaders(HttpStatusCode.InternalServerError, processId);
                result = ex.GetResponseContent();
            }
            finally
            {
                var currentStatusCode = (HttpStatusCode)context.Response.StatusCode;
                await JCatMiddlewareUtil.SetOriginalRespBodyStream(currentStatusCode, result, originalRespBodyStream);
            }
        }
    }
}
