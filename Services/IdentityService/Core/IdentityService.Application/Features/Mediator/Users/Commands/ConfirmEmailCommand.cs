using MediatR;

namespace IdentityService.Application.Commands;

public class ConfirmEmailCommand : IRequest<bool>
{
    public string Email { get; set; } = null!;
    public string VerificationCode { get; set; } = null!;
}
