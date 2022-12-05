using JCat.Notification.Email.Gamail.Const;
using JCat.Notification.Email.Gamail.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace JCat.Notification.Email.Gamail;

public static class GmailExtension
{
    public static void AddGmailClient(this IServiceCollection services, SMTPOption option, EmailDefaultOption? emailDefaultOption = null)
    {
        option.IsNullSetDefault();
        services.AddEmailClient(option, emailDefaultOption);
        services.AddScoped<IGmailClient, GmailClient>();
    }

    private static void IsNullSetDefault(this SMTPOption option)
    {
        if (string.IsNullOrWhiteSpace(option.Host))
        {
            option.Host = SettingsConst.GmailHost;
        }

        if (option.Port == null)
        {
            option.Port = SettingsConst.GmailPort;
        }
    }
}