namespace JCat.Notification.Email.Message;
internal static class ValidationMessage
{
    public const string MailSenderNotFoundError = "[Mail Sender] can not be null.";
    public const string MailFromNotFoundError = "[Mail From] can not be null.";
    public const string MailToNotFoundError = "[Mail To] can not be null.";
    public const string SMTPOptionNotFoundError = "SMTP Options can not be null or white-space.";
}