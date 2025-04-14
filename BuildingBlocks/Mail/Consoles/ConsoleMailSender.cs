using System;
using System.Threading.Tasks;

namespace BuildingBlocks.Mail.Consoles;

public class ConsoleMailSender /*: IMailSender*/
{
    public Task SendAsync(string to, string subject, string body)
    {
        Console.WriteLine($"[Mail] To: {to} | Subject: {subject} | Body: {body}");
        return Task.CompletedTask;
    }
}

