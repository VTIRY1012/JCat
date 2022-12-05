using JCat.Notification.Email.Gamail.Interface;
using JCat.Notification.Email.Interface;
using System.Net.Mail;

namespace JCat.Notification.Email.Gamail;

public class GmailClient : IGmailClient
{
    private readonly IEmailClient _emailClient;
    public GmailClient(IEmailClient emailClient)
    {
        _emailClient = emailClient;
    }

    public async Task<(bool isSuccess, string message)> EnterVerificationCodeAsync(string receiverMail, string code)
    {
        var message = BaseMailMessage();
        message.Subject = "Your e-mail verification code";
        message.Body = HtmlHelper.GetVerificationCodeHtml(receiverMail, code);
        message.To.Add(receiverMail);
        var result = await _emailClient.SendMailAsync(message);
        return result;
    }

    public async Task<(bool isSuccess, string message)> ForgotPasswordAsync(string receiverMail, string newPassword)
    {
        var message = BaseMailMessage();
        message.Subject = "Forgot your password?";
        message.Body = HtmlHelper.GetForgotPasswordHtml(receiverMail, newPassword);
        message.To.Add(receiverMail);
        var result = await _emailClient.SendMailAsync(message);
        return result;
    }

    private MailMessage BaseMailMessage()
    {
        var message = new MailMessage();
        message.ProjectSender();
        message.ProjectEncoder();
        message.ProjectIsBodyHtml();
        message.Priority = MailPriority.High;
        return message;
    }
}