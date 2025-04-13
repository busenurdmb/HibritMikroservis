namespace NotificationService.Domain.Enitites;

public class MailLog
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public DateTime SentAt { get; set; }
}
