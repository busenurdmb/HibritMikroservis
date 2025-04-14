namespace BuildingBlocks.Mail.Smtp;

public class MailSettings
{
    public string SmtpServer { get; set; } = null!;
    public int SmtpPort { get; set; }
    public string SenderName { get; set; } = null!;
    public string SenderEmail { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
