namespace BuildingBlocks.Mail;

public interface IMailSender
{
    Task SendAsync(string to, string subject, string body);
}
