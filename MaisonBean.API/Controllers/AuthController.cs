using MaisonBean.Application.Auth.Commands;
using MaisonBean.Application.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace MaisonBean.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly IAuthService _authService;

    private readonly IWebHostEnvironment _environment;

    public AuthController(
        IMediator mediator,
        IAuthService authService,
        IWebHostEnvironment environment)
    {
        _mediator = mediator;

        _authService = authService;

        _environment = environment;
    }

    // =====================================================
    // REGISTER
    // =====================================================

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommand cmd)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                success = false,

                errors = ModelState
                    .Where(x =>
                        x.Value.Errors.Count > 0)

                    .ToDictionary(
                        kvp => kvp.Key,

                        kvp => kvp.Value.Errors
                            .Select(e =>
                                e.ErrorMessage)
                    )
            });
        }

        var result =
            await _mediator.Send(cmd);

        if (!result.Success)
        {
            return Conflict(new
            {
                success = false,

                message =
                    result.Message
            });
        }

        return StatusCode(201, new
        {
            success = true,

            message =
                result.Message
        });
    }

    // =====================================================
    // LOGIN
    // =====================================================

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand cmd)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                success = false,

                errors = ModelState
                    .Where(x =>
                        x.Value.Errors.Count > 0)

                    .ToDictionary(
                        kvp => kvp.Key,

                        kvp => kvp.Value.Errors
                            .Select(e =>
                                e.ErrorMessage)
                    )
            });
        }

        var result =
            await _mediator.Send(cmd);

        if (!result.Success)
        {
            return Unauthorized(new
            {
                success = false,

                message =
                    result.Message
            });
        }

        // =================================================
        // ACCESS TOKEN COOKIE
        // =================================================

        Response.Cookies.Append(
            "accessToken",
            result.Token!,
            CreateAuthCookieOptions(
                DateTime.UtcNow
                    .AddMinutes(15)
            ));

        // =================================================
        // REFRESH TOKEN COOKIE
        // =================================================

        Response.Cookies.Append(
            "refreshToken",
            result.RefreshToken!,
            CreateAuthCookieOptions(
                DateTime.UtcNow
                    .AddDays(7)
            ));

        return Ok(new
        {
            success = true,

            message =
                "Login successful",

            user = result.User
        });
    }

    // =====================================================
    // REFRESH TOKEN
    // =====================================================

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken =
            Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(new
            {
                success = false,

                message =
                    "Refresh token missing"
            });
        }

        var result =
            await _authService
                .RefreshTokenAsync(
                    refreshToken
                );

        if (!result.Success)
        {
            return Unauthorized(new
            {
                success = false,

                message =
                    result.Message
            });
        }

        // =================================================
        // ACCESS TOKEN COOKIE
        // =================================================

        Response.Cookies.Append(
            "accessToken",
            result.Token!,
            CreateAuthCookieOptions(
                DateTime.UtcNow
                    .AddMinutes(15)
            ));

        // =================================================
        // REFRESH TOKEN COOKIE
        // =================================================

        Response.Cookies.Append(
            "refreshToken",
            result.RefreshToken!,
            CreateAuthCookieOptions(
                DateTime.UtcNow
                    .AddDays(7)
            ));

        return Ok(new
        {
            success = true,

            message =
                "Token refreshed"
        });
    }

    // =====================================================
    // LOGOUT
    // =====================================================

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        CancellationToken ct)
    {
        var userId =
    User.FindFirstValue("id");

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new
            {
                success = false,

                message =
                    "User not authenticated"
            });
        }

        if (!int.TryParse(userId, out var parsedUserId))
        {
            return Unauthorized(new
            {
                success = false,

                message =
                    "User not authenticated"
            });
        }

        var result =
            await _mediator.Send(
                new LogoutCommand
                {
                    UserId =
                        parsedUserId
                },
                ct);

        if (!result)
        {
            return BadRequest(new
            {
                success = false,

                message =
                    "Logout failed"
            });
        }

        // =================================================
        // DELETE COOKIES
        // =================================================

        Response.Cookies.Delete(
            "accessToken",
            CreateDeleteCookieOptions());

        Response.Cookies.Delete(
            "refreshToken",
            CreateDeleteCookieOptions());

        return Ok(new
        {
            success = true,

            message =
                "Logged out successfully"
        });
    }

    private CookieOptions CreateAuthCookieOptions(
        DateTime expires)
    {
        var secure =
            ShouldUseSecureCookies();

        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7)
        };
    }

    private CookieOptions CreateDeleteCookieOptions()
    {
        var secure =
            ShouldUseSecureCookies();

        return new CookieOptions
        {
            HttpOnly = true,

            Secure = true,

            SameSite = SameSiteMode.None,

            Expires = DateTime.UtcNow.AddDays(7)
        };

    }

    private bool ShouldUseSecureCookies()
    {
        return !_environment.IsDevelopment();
    }
}
