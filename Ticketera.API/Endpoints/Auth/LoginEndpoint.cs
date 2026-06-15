using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketera.Application.UseCases.Commands.Auth.Login;

namespace Ticketera.API.Endpoints.Auth;

[Route("api/auth")]
[ApiController]
public class LoginEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public LoginEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> HandleAsync([FromBody] LoginCommand command)
    {
        // Se envía el comando directamente al Handler correspondiente en Application
        var token = await _mediator.Send(command);

        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
        }

        return Ok(new { token });
    }
}