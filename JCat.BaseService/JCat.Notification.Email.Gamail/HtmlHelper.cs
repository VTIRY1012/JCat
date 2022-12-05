namespace JCat.Notification.Email.Gamail;
internal static class HtmlHelper
{
    public static string GetVerificationCodeHtml(string email, string code)
    {
        string html = LoadVerificationCodeHtml();
        html = html
            .Replace("{{receiverMail}}", email)
            .Replace("{{code}}", code);
        return html;
    }
    public static string GetForgotPasswordHtml(string email, string newPassword)
    {
        string html = LoadForgotPasswordHtml();
        html = html
            .Replace("{{receiverMail}}", email)
            .Replace("{{newPassword}}", newPassword);
        return html;
    }

    #region Private
    private static string _verificationCodeHtml = "";
    private static string LoadVerificationCodeHtml()
    {
        if (string.IsNullOrWhiteSpace(_verificationCodeHtml))
        {
            _verificationCodeHtml = LoadHtml("VerificationCode.html");
        }

        return _verificationCodeHtml;
    }
    private static string _forgotPasswordHtml = "";
    private static string LoadForgotPasswordHtml()
    {
        if (string.IsNullOrWhiteSpace(_forgotPasswordHtml))
        {
            _forgotPasswordHtml = LoadHtml("ForgotPassword.html");
        }

        return _forgotPasswordHtml;
    }
    private static string LoadHtml(string fileName)
    {
        try
        {
            var folder = $"{AppDomain.CurrentDomain.BaseDirectory}/Html";
            var path = Path.Combine(folder, fileName);
            using var sr = new StreamReader(path);
            var text = sr.ReadToEnd();
            return text;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
}