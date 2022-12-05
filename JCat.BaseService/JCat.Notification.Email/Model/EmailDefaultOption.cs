using System.Text;

namespace JCat.Notification.Email;

public class EmailDefaultOption
{
    public Encoding Encoder { get; set; } = Encoding.UTF8;
    public bool IsBodyHtml { get; set; } = true;
    public string Sender { get; set; } = string.Empty;
}