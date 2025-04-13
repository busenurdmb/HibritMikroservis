using Microsoft.EntityFrameworkCore;

namespace NotificationService.Infrastructure;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

    public DbSet<MailLog> MailLogs => Set<MailLog>();
}

public class MailLog
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public DateTime SentAt { get; set; }
}
