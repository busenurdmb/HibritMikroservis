using BuildingBlocks.EventBus;
using IdentityService.Application.Commands;
using IdentityService.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Events;

namespace IdentityService.Application.Handlers;

public class ConfirmEmailHandler(AppDbContext dbContext, IEventPublisher eventPublisher) : IRequestHandler<ConfirmEmailCommand, bool>
{
    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null || user.IsVerified || user.VerificationCode != request.VerificationCode)
            return false;

        user.IsVerified = true;

        await dbContext.SaveChangesAsync(cancellationToken);

        // EVENT: Yayınla
        eventPublisher.Publish(new UserVerifiedIntegrationEvent
        {
            Email = user.Email
        });

        // TODO: RabbitMQ event gönderilecek
        Console.WriteLine($"[Event Simülasyonu] Kullanıcı onaylandı: {user.Email} → Hoş geldiniz maili gönderilsin!");

        return true;
    }
}
