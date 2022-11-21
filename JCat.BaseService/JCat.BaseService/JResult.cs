using JCat.BaseService.Const;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace JCat.BaseService
{
    public sealed class JResult
    {
        public JResult()
        {

        }

        public JResult(HttpStatusCode code, object? data)
        {
            this.Code = code;
            this.Data = data;
        }

        public JResult(HttpStatusCode code, string message, string ErrorMessage, object? data)
        {
            this.Code = code;
            this.Message = message;
            this.ErrorMessage = ErrorMessage;
            this.Data = data;
        }

        /// <summary>
        /// Return Http Status
        /// </summary>
        public HttpStatusCode? Code { get; set; }

        /// <summary>
        /// Response
        /// </summary>
        public Object? Data { get; set; }

        /// <summary>
        /// Response Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Response Backend Message
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public sealed class JHttpContextResult
    {
        public JHttpContextRequestResult RequestResult =
            new JHttpContextRequestResult();
        public JHttpContextResponseResult ResponseResult =
            new JHttpContextResponseResult();
    }

    public sealed class JHttpContextRequestResult
    {
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> QueryString { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; } = StringConst.Empty;
        public Dictionary<string, string> FormValues { get; set; } = new Dictionary<string, string>();
        public string Method { get; set; } = StringConst.Empty;
        public string Path { get; set; } = StringConst.Empty;
        public HostString Host { get; set; }
        public string Scheme { get; set; } = StringConst.Empty;
    }

    public sealed class JHttpContextResponseResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string ContentType { get; set; } = StringConst.Empty;
        public object? Result { get; set; }
    }
}
