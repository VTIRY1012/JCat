using JCat.Notification.Email.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;

namespace JCat.Notification.Email;

public static class EmailExtension
{
    public static void AddEmailClient(this IServiceCollection services, SMTPOption option, EmailDefaultOption? emailDefaultOption = null)
    {
        EmailConfig.InitializeSettings(option, emailDefaultOption);
        services.AddScoped<IEmailClient, EmailClient>();
    }
}

public static class EmailDefaultExtension
{
    internal static void AddDefaultSMTP(this SmtpClient smtpClient)
    {
        smtpClient.Host = EmailConfig.smtpOption.Host;
        smtpClient.Port = EmailConfig.smtpOption.Port.Value;
    }

    public static MailMessage ProjectEncoder(this MailMessage message)
    {
        var option = EmailConfig.EmailDefaultOption;
        message.HeadersEncoding = option.Encoder;
        message.SubjectEncoding = option.Encoder;
        message.BodyEncoding = option.Encoder;
        return message;
    }

    public static MailMessage ProjectSender(this MailMessage message)
    {
        var option = EmailConfig.EmailDefaultOption;
        message.From = new MailAddress(option.Sender);
        message.Sender = new MailAddress(option.Sender);
        return message;
    }

    public static MailMessage ProjectIsBodyHtml(this MailMessage message)
    {
        var option = EmailConfig.EmailDefaultOption;
        message.IsBodyHtml = option.IsBodyHtml;
        return message;
    }
}