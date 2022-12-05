using System.Net;

namespace JCat.Notification.Email;

public static class EmailConfig
{
    public static EmailDefaultOption EmailDefaultOption { get; private set; } = new EmailDefaultOption();
    internal static SMTPOption smtpOption { get; private set; } = new SMTPOption();
    internal static NetworkCredential NetworkCredential
    {
        get => new NetworkCredential()
        {
            UserName = smtpOption.Account,
            Password = smtpOption.Password
        };
    }

    internal static void InitializeSettings(SMTPOption option, EmailDefaultOption? emailDefaultOption = null)
    {
        smtpOption = option;
        if (emailDefaultOption != null)
        {
            EmailDefaultOption = emailDefaultOption;
        }
        else
        {
            EmailDefaultOption.Sender = option.Account;
        }
    }
}