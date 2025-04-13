using FluentValidation;

namespace IdentityService.Application.Commands;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email girin.");

        RuleFor(x => x.VerificationCode)
            .NotEmpty().WithMessage("Doğrulama kodu zorunludur.")
            .Length(6).WithMessage("Kod 6 haneli olmalıdır.");
    }
}
