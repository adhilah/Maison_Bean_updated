using MaisonBean.Application.Interfaces;
using MediatR;

namespace MaisonBean.Application.Auth.Commands;

//LoginCommand
public class LoginCommand : IRequest<LoginResult>
{
    public string Email { get; set; }
        = string.Empty;

    public string Password { get; set; }
        = string.Empty;
}

//LoginResult
public class LoginResult
{
    public bool Success { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public string? Message { get; set; }

    public UserDto? User { get; set; }
}

//LoginHandler
public class LoginCommandHandler
    : IRequestHandler<
        LoginCommand,
        LoginResult>
{
    private readonly IAuthService
        _authService;

    private readonly ICookieService
        _cookieService;

    public LoginCommandHandler(
        IAuthService authService,
        ICookieService cookieService)
    {
        _authService = authService;

        _cookieService = cookieService;
    }

    public async Task<LoginResult> Handle(
        LoginCommand request,
        CancellationToken ct)
    {
        var result =
            await _authService.LoginAsync(
                request,
                ct);

        if (!result.Success)
        {
            return result;
        }

        _cookieService.SetAuthCookies(
            result.Token!,
            result.RefreshToken!);

        return result;
    }
}