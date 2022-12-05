namespace JCat.Notification.Email.Gamail.Interface;

public interface IGmailClient
{
    Task<(bool isSuccess, string message)> EnterVerificationCodeAsync(string receiverMail, string code);
    Task<(bool isSuccess, string message)> ForgotPasswordAsync(string receiverMail, string newPassword);
}