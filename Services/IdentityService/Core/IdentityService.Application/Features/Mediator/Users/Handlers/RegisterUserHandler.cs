using IdentityService.Application.Commands;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Handlers;

public class RegisterUserHandler(AppDbContext dbContext) : IRequestHandler<RegisterUserCommand, bool>
{
    public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Aynı email varsa kayıt etme
        if (await dbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
            return false;

        // Basit doğrulama kodu (6 haneli sayı)
        var verificationCode = new Random().Next(100000, 999999).ToString();

        var user = new User
        {
            Email = request.Email,
            Password = request.Password, // TODO: Şifreyi hashle!
            VerificationCode = verificationCode,
            IsVerified = false
        };

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        // TODO: Mail gönderme işlemi burada yapılacak
        Console.WriteLine($"[Mail Simülasyonu] {request.Email} adresine doğrulama kodu gönderildi: {verificationCode}");

        return true;
    }
}
