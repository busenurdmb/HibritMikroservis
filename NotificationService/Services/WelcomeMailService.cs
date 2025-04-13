namespace NotificationService.Services;

public class WelcomeMailService
{
    public Task SendWelcomeMail(string email)
    {
        // TODO: SMTP mail gönderme işlemi buraya gelecek
        Console.WriteLine($"[Mail Gitti] {email} adresine hoş geldiniz maili gönderildi.");
        return Task.CompletedTask;
    }
}
