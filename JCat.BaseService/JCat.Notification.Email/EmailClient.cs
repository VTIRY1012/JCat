using JCat.Notification.Email.Interface;
using JCat.Notification.Email.Message;
using System.Net.Mail;

namespace JCat.Notification.Email;
public class EmailClient : IEmailClient
{
    public EmailClient()
    {
        EmailValidation.EnsureEmailOption();
    }

    public async Task<(bool isSuccess, string message)> SendMailAsync(MailMessage message)
    {
        try
        {
            message
                .EnsureMailSender()
                .EnsureMailFrom()
                .EnsureMailTo();

            using var smtp = new SmtpClient();
            smtp.AddDefaultSMTP();
            smtp.Credentials = EmailConfig.NetworkCredential;
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(message);
            return (true, SystemMessage.Successed);
        }
        catch (Exception ex)
        {
            return (false, SystemMessage.Exception(ex.Message, ex.StackTrace));
        }
    }
}