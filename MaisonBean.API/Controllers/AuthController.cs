using MaisonBean.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaisonBean.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    // REGISTER
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommand command)
    {
        var result =
            await _mediator.Send(command);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    // LOGIN
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command)
    {
        var result =
            await _mediator.Send(command);

        if (!result.Success)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    // REFRESH TOKEN
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var result =
            await _mediator.Send(
                new RefreshTokenCommand()
            );

        if (!result.Success)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }

    // LOGOUT
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var result =
            await _mediator.Send(
                new LogoutCommand()
            );

        if (!result)
        {
            return BadRequest();
        }

        return Ok(new
        {
            message =
                "Logged out successfully"
        });
    }
}