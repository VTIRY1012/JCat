using JCat.Notification.Email.Message;
using System.Net.Mail;

namespace JCat.Notification.Email;

internal class EmailValidation
{
    public static void EnsureEmailOption()
    {
        var option = EmailConfig.smtpOption;
        if (option == null ||
            string.IsNullOrWhiteSpace(option.Host) ||
            (option.Port == null) ||
            string.IsNullOrWhiteSpace(option.Account) ||
            string.IsNullOrWhiteSpace(option.Password))
        {
            throw new ArgumentNullException(ValidationMessage.SMTPOptionNotFoundError);
        }
    }
}

internal static class EmailValidationExtension
{
    internal static MailMessage EnsureMailSender(this MailMessage mailMessage)
    {
        if (mailMessage.Sender == null)
        {
            throw new ArgumentNullException(ValidationMessage.MailSenderNotFoundError);
        }

        return mailMessage;
    }

    internal static MailMessage EnsureMailFrom(this MailMessage mailMessage)
    {
        if (mailMessage.From == null)
        {
            throw new ArgumentNullException(ValidationMessage.MailFromNotFoundError);
        }

        return mailMessage;
    }

    internal static MailMessage EnsureMailTo(this MailMessage mailMessage)
    {
        if (mailMessage.From == null)
        {
            throw new ArgumentNullException(ValidationMessage.MailToNotFoundError);
        }

        return mailMessage;
    }
}