using JCat.BaseService.Extensions.BaseType;

namespace JCat.BaseService
{
    public sealed class JErrorResult
    {
        public JErrorResult() { }
        public JErrorResult(string message, string errorMessage)
        {
            this.Message = message.IfNullToEmpty();
            this.ErrorMessage = errorMessage.IfNullToEmpty();
        }
        public JErrorResult(string message, string errorMessage, object? data)
        {
            this.Message = message.IfNullToEmpty();
            this.ErrorMessage = errorMessage.IfNullToEmpty();
            this.Data = data;
        }

        /// <summary>
        /// 1. Success Content.
        /// 2. Error Extra Message.
        /// </summary>
        public Object? Data { get; set; }
        /// <summary>
        /// Frontend Message
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Backend Message
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
