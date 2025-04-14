using MediatR;

namespace IdentityService.Application.Commands;

public class RegisterUserCommand : IRequest<bool>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
