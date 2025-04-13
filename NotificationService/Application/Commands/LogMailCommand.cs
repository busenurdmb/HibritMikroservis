using MediatR;

namespace NotificationService.Application.Commands;

public class LogMailCommand : IRequest
{
    public string Email { get; set; } = null!;
    public DateTime SentAt { get; set; }
}
