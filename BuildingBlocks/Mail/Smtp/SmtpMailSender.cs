using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using BuildingBlocks.Mail.Smtp;

namespace BuildingBlocks.Mail;

public class SmtpMailSender : IMailSender
{
    private readonly MailSettings _settings;

    public SmtpMailSender(IOptions<MailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_settings.Username, _settings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        Console.WriteLine($"[Mail] To: {to} | Subject: {subject} | Body: {body}");
    }
}
