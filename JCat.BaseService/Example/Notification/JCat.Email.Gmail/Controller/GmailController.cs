using JCat.BaseService;
using JCat.Notification.Email.Gamail.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace JCat.Email.Gmail.Api.Controller;

[ApiController]
[Route("Gmail")]
public class GmailController : BaseServiceController
{
    private readonly IGmailClient _gmailClient;
    public GmailController(IGmailClient gmailClient)
    {
        _gmailClient = gmailClient;
    }

    // todo: add your email
    private const string email = "";

    [HttpPost]
    [Route("VerificationCode")]
    public async Task<JResult> VerificationCodeAsync()
    {
        var code = GetNumber(6);
        var result = await _gmailClient.EnterVerificationCodeAsync(email, code);
        return Successed(result.message);
    }

    [HttpPost]
    [Route("ForgotPassword")]
    public async Task<JResult> Async()
    {
        var password = GetNumber(8);
        var result = await _gmailClient.ForgotPasswordAsync(email, password);
        return Successed(result.message);
    }

    private string GetNumber(int codeLength)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < codeLength; i++)
        {
            var rand = new Random();
            sb.Append(rand.Next(0, 10));
        }
        var code = sb.ToString();
        return code;
    }
}