using MediatR;
using NotificationService.Application.Commands;
using NotificationService.Domain;
using NotificationService.Infrastructure;

namespace NotificationService.Application.Handlers;

public class LogMailCommandHandler(NotificationDbContext dbContext) : IRequestHandler<LogMailCommand>
{
    public async Task<Unit> Handle(LogMailCommand request, CancellationToken cancellationToken)
    {
        dbContext.MailLogs.Add(new MailLog
        {
            Email = request.Email,
            SentAt = request.SentAt
        });

        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
