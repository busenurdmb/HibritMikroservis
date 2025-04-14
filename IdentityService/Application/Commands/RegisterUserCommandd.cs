using MediatR;

namespace IdentityService.Application.Commands;

public class RegisterUserCommandd : IRequest<bool>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
