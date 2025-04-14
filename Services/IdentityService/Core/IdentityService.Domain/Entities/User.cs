namespace IdentityService.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsVerified { get; set; } = false;
    public string VerificationCode { get; set; } = null!;
}
