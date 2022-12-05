namespace JCat.Notification.Email.Message;

internal static class SystemMessage
{
    public const string Successed = "Successed.";
    public static string Exception(string message, string? stackTrace) => $"Message: {message}, StackTrace: {stackTrace}";
}