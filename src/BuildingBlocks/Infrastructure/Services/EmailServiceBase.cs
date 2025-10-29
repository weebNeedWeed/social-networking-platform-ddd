using BuildingBlocks.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BuildingBlocks.Infrastructure.Services;

public class EmailServiceBase : IEmailServiceBase
{
    public MailboxAddress From => new MailboxAddress("PhotoLite", _emailOptions.UserName);

    private readonly EmailOptions _emailOptions;

    public EmailServiceBase(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task<SmtpClient> GetClientAsync()
    {
        var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_emailOptions.Host, _emailOptions.Port);
        await smtpClient.AuthenticateAsync(_emailOptions.UserName, _emailOptions.Password);

        return smtpClient;
    }
}