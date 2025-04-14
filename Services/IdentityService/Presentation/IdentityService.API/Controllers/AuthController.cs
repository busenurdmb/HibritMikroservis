using IdentityService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var result = await mediator.Send(command);
        if (!result)
            return BadRequest("Bu email adresi zaten kayıtlı.");

        return Ok("Kayıt başarılı. Doğrulama kodu e-posta adresinize gönderildi.");
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
    {
        var result = await mediator.Send(command);
        if (!result)
            return BadRequest("Doğrulama başarısız. Kod veya e-posta hatalı olabilir.");

        return Ok("Hesabınız başarıyla doğrulandı.");
    }

}
