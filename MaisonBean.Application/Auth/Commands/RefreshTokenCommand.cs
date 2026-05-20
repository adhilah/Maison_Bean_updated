//using MaisonBean.Application.Auth.Commands;
//using MaisonBean.Application.Interfaces;
//using MediatR;

//namespace MaisonBean.Application.Auth.Commands;

//public class RefreshTokenCommand : IRequest<LoginResult>
//{
//    public string RefreshToken { get; set; } = string.Empty;
//}

//public class RefreshTokenCommandHandler
//    : IRequestHandler<RefreshTokenCommand, LoginResult>
//{
//    private readonly IAuthService _authService;

//    public RefreshTokenCommandHandler(IAuthService authService)
//    {
//        _authService = authService;
//    }

//    public async Task<LoginResult> Handle(
//        RefreshTokenCommand request,
//        CancellationToken ct)
//    {
//        return await _authService.RefreshTokenAsync(
//            request.RefreshToken
//        );
//    }
//}






//============================================================================================================
using MaisonBean.Application.Interfaces;

using MediatR;

namespace MaisonBean.Application.Auth.Commands;

public class RefreshTokenCommand
    : IRequest<LoginResult>
{
}

public class RefreshTokenCommandHandler
    : IRequestHandler<
        RefreshTokenCommand,
        LoginResult>
{
    private readonly IAuthService
        _authService;

    private readonly ICookieService
        _cookieService;

    public RefreshTokenCommandHandler(
        IAuthService authService,
        ICookieService cookieService)
    {
        _authService =
            authService;

        _cookieService =
            cookieService;
    }

    public async Task<LoginResult> Handle(
        RefreshTokenCommand request,
        CancellationToken ct)
    {
        var refreshToken =
            _cookieService
                .GetRefreshToken();

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return new LoginResult
            {
                Success = false,

                Message =
                    "Refresh token missing"
            };
        }

        var result =
            await _authService
                .RefreshTokenAsync(
                    refreshToken);

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