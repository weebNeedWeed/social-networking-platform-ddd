using System.Reflection;
using System.Text;
using BuildingBlocks.Application.Common.Interfaces;
using MimeKit;
using Modules.IAM.Application.Common.Interfaces.Services;
using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Infrastructure.Services;

public class IAMEmailService : IIAMEmailService
{
    private readonly IEmailServiceBase _emailServiceBase;

    public IAMEmailService(IEmailServiceBase emailServiceBase)
    {
        _emailServiceBase = emailServiceBase;
    }

    public async Task SendActivationEmailAsync(UserAccount userAccount)
    {
        var message = new MimeMessage();

        var to = new MailboxAddress("", userAccount.Email);
        message.To.Add(to);
        message.From.Add(_emailServiceBase.From);

        message.Subject = "Verify Your Email Address";

        var emailContent = GetActivationEmailTemplate();
        emailContent = emailContent.Replace("{{USERNAME}}", userAccount.UserName);

        message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = emailContent.ToString(),
        };

        var client = await _emailServiceBase.GetClientAsync();
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    private StringBuilder GetActivationEmailTemplate()
    {
        var thisAssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var templateFile = Path.Combine(thisAssemblyDir!, "EmailTemplates", "ActivationEmail.html");

        var templateContent = File.ReadAllText(templateFile);

        return new StringBuilder(templateContent);
    } 
}