using JCat.BaseService.Const;
using JCat.BaseService.Converter;
using JCat.BaseService.JCatException;
using System.Diagnostics.CodeAnalysis;

namespace JCat.BaseService.Extensions
{
    public static class BadRequest
    {
        // Get BadRequestException.
        public static BadRequestException BadRequestException(this string message)
        {
            return GetBadRequest(message, null);
        }

        public static BadRequestException BadRequestException(this string message, object data)
        {
            return GetBadRequest(message, data);
        }

        public static BadRequestException BadRequestException(this string message, object data, string errorMessage)
        {
            return GetBadRequest(message, data, errorMessage);
        }
        // Throw BadRequestException.
        public static void ThrowBadRequestException(this string message)
        {
            ThrowBadRequest(message, null);
        }

        public static void ThrowBadRequestException(this string message, object? data)
        {
            ThrowBadRequest(message, data);
        }

        public static void ThrowBadRequestException(this string message, object? data, string errorMessage)
        {
            ThrowBadRequest(message, data, errorMessage);
        }

        public static void ThrowBadRequestExceptionIfTrue<TData>(this bool condition, string message, TData data)
        {
            if (condition)
            {
                ThrowBadRequestException(message, data);
            }
        }

        public static void ThrowBadRequestExceptionIfFalse<TData>(this bool condition, string message, TData data)
        {
            if (!condition)
            {
                ThrowBadRequestException(message, data);
            }
        }
        // BadRequest Response 
        [return: MaybeNull]
        public static JErrorResult GetResponseContent(this BadRequestException exception)
        {
            if (string.IsNullOrWhiteSpace(exception?.Message))
            {
                return new JErrorResult();
            }

            return JCatSerializer.Deserialize<JErrorResult>(exception.Message);
        }

        #region Private
        private static void ThrowBadRequest(string message, object? data, string errorMessage = StringConst.Empty)
        {
            var exception = GetBadRequest(message, data, errorMessage);
            throw exception;
        }

        private static BadRequestException GetBadRequest(string message, object? data, string errorMessage = StringConst.Empty)
        {
            return new BadRequestException(new JErrorResult()
            {
                Message = message,
                ErrorMessage = errorMessage,
                Data = data
            });
        }

        #endregion
    }
}
