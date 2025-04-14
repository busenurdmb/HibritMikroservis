using BuildingBlocks.Mail;
using MediatR;
using NotificationService.Application.Commands;


public class WelcomeMailService(IMailSender mailSender, IMediator mediator)
{
    public async Task SendWelcomeMail(string email)
    {
        var subject = "Hoş Geldiniz!";
        var body = "Sisteme başarıyla giriş yaptınız.";
        await mailSender.SendAsync(email, subject, body);

        // Veritabanına log kaydet
        await mediator.Send(new LogMailCommand
        {
            Email = email,
            SentAt = DateTime.UtcNow
        });
    }
}
   