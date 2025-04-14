using FluentValidation;

namespace IdentityService.Application.Commands;

public class RegisterUserValidatorr : AbstractValidator<RegisterUserCommandd>
{
    public RegisterUserValidatorr()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email girin.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre zorunludur.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.");
    }
}
