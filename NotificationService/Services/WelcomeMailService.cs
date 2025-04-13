using BuildingBlocks.Mail;
using NotificationService.Infrastructure;

public class WelcomeMailService(IMailSender mailSender, NotificationDbContext dbContext)
{
    public async Task SendWelcomeMail(string email)
    {
        var subject = "Hoş Geldiniz!";
        var body = "Sisteme başarıyla giriş yaptınız.";
        await mailSender.SendAsync(email, subject, body);

        // Veritabanına log kaydet
        var log = new MailLog
        {
            Email = email,
            SentAt = DateTime.UtcNow
        };

        dbContext.MailLogs.Add(log);
        await dbContext.SaveChangesAsync();
    }
}
   