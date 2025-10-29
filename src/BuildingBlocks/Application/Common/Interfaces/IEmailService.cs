using MailKit.Net.Smtp;
using MimeKit;

namespace BuildingBlocks.Application.Common.Interfaces;

public interface IEmailServiceBase
{
    MailboxAddress From { get; }

    Task<SmtpClient> GetClientAsync();
}