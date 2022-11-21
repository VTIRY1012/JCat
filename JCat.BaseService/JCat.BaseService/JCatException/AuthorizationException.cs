namespace JCat.BaseService.JCatException
{
    public class AuthorizationException : SystemException
    {
        public AuthorizationException() { }
        public AuthorizationException(string message) : base(message) { }
        public AuthorizationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
