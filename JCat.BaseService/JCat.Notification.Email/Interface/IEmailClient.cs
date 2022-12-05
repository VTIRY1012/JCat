using System.Net.Mail;

namespace JCat.Notification.Email.Interface;
public interface IEmailClient
{
    Task<(bool isSuccess, string message)> SendMailAsync(MailMessage message);
}