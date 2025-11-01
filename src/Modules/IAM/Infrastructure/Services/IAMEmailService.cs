using System.Reflection;
using System.Text;
using BuildingBlocks.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using MimeKit;
using Modules.IAM.Application.Common.Interfaces.Services;
using Modules.IAM.Domain.UserAccount;

namespace Modules.IAM.Infrastructure.Services;

public class IAMEmailService : IIAMEmailService
{
    private readonly IEmailServiceBase _emailServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IAMEmailService(IEmailServiceBase emailServiceBase, IHttpContextAccessor httpContextAccessor)
    {
        _emailServiceBase = emailServiceBase;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SendActivationEmailAsync(Guid userId, string userName, string email, string token)
    {
        var message = new MimeMessage();

        var to = new MailboxAddress("", email);
        message.To.Add(to);
        message.From.Add(_emailServiceBase.From);

        message.Subject = "Verify Your Email Address";

        var activateLink = string.Format("{0}://{1}/auth/activate?userId={2}&token={3}",
            _httpContextAccessor.HttpContext.Request.Scheme,
            _httpContextAccessor.HttpContext.Request.Host,
            userId,
            token);

        var emailContent = GetActivationEmailTemplate();
        emailContent = emailContent.Replace("{{USERNAME}}", userName);
        emailContent = emailContent.Replace("{{ACTIVATION_LINK}}", activateLink);

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