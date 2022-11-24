using JCat.BaseService.Extensions.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;

namespace JCat.BaseService.Middleware
{
    public class JLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public JLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<JLoggerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            // Request.
            var apiContext = new JHttpContextResult();
            apiContext.RequestResult = GetRequest(context);
            // Do method.
            var respBody = await Next(context);
            // Response.
            apiContext.ResponseResult = GetResponse(context);
            apiContext.ResponseResult.Result = respBody;
            var t = JsonSerializer.Serialize(apiContext);

            var statusCode = apiContext.ResponseResult.StatusCode;
            if (statusCode.IsSuccessStatusCode())
            {
                _logger.LogInformation(LogMessage(apiContext));
            }
            else if (statusCode.IsBadRequestStatusCode())
            {
                _logger.LogWarning(LogMessage(apiContext));
            }
            else if (statusCode.IsSystemErrorStatusCode())
            {
                _logger.LogError(LogMessage(apiContext));
            }
        }

        private async Task<string> Next(HttpContext context)
        {
            Stream originalRespBodyStream = context.Response.Body;
            await using var ms = new MemoryStream();
            context.Response.Body = ms;

            await _next(context);

            ms.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(ms);
            var respContent = await streamReader.ReadToEndAsync();
            ms.Seek(0, SeekOrigin.Begin);

            var bytes = Encoding.UTF8.GetBytes(respContent);
            await originalRespBodyStream.WriteAsync(bytes, 0, bytes.Length);

            return respContent;
        }

        private JHttpContextRequestResult GetRequest(HttpContext context)
        {
            var httpRequest = context.Request;
            var result = new JHttpContextRequestResult();
            result.Headers = httpRequest.Headers.AsDictionary();
            if (httpRequest.HasFormContentType)
            {
                result.FormValues = httpRequest.Form.AsDictionary();
            }
            if (httpRequest.QueryString.HasValue)
            {
                result.QueryString = httpRequest.Query.AsDictionary();
            }
            result.Body = httpRequest.GetBodyString();
            result.Host = httpRequest.Host;
            result.Scheme = httpRequest.Scheme;
            result.Path = httpRequest.Path;
            result.Method = httpRequest.Method;
            return result;
        }

        private JHttpContextResponseResult GetResponse(HttpContext context)
        {
            var httpResponse = context.Response;
            var result = new JHttpContextResponseResult()
            {
                ContentType = httpResponse.ContentType,
                StatusCode = (HttpStatusCode)httpResponse.StatusCode,
                Headers = httpResponse.Headers.AsDictionary()
            };

            return result;
        }

        private string LogMessage(JHttpContextResult result)
        {
            return $"Path: {result.RequestResult.Path} , Content: {result.ToNewtonJsonString()}";
        }
    }
}
