using BuildingBlocks.Application.Common.Interfaces;
using MimeKit;
using Modules.IAM.Application.Common.Interfaces.Services;

namespace Modules.IAM.Infrastructure.Services;

public class IAMEmailService : IIAMEmailService
{
    private readonly IEmailServiceBase _emailServiceBase;

    public IAMEmailService(IEmailServiceBase emailServiceBase)
    {
        _emailServiceBase = emailServiceBase;
    }

    public async Task SendActivationEmailAsync(string email, string activationToken)
    {
        await Task.CompletedTask;
        var message = new MimeMessage();

        var to = new MailboxAddress("", email);
        message.To.Add(to);
        message.From.Add(_emailServiceBase.From);

        return;
    }
}