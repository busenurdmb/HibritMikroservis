using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Enitites;

namespace NotificationService.Infrastructure;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

    public DbSet<MailLog> MailLogs => Set<MailLog>();
}


