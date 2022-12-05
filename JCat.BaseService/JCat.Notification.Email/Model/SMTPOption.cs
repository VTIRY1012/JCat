public class SMTPOption
{
    public SMTPOption() { }

    public SMTPOption(string account, string password)
    {
        this.Account = account;
        this.Password = password;
    }

    public SMTPOption(string host, int? port, string account, string password)
    {
        this.Host = host;
        this.Port = port;
        this.Account = account;
        this.Password = password;
    }

    public string Host { get; set; } = string.Empty;
    public int? Port { get; set; }
    public string Password { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
}