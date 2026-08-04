using MaisonBean.Application.Interfaces;
using MediatR;

namespace MaisonBean.Application.Auth.Commands;



//LogoutCommand
public class LogoutCommand
    : IRequest<bool>
{
}

//LogoutHandler
public class LogoutCommandHandler
    : IRequestHandler<
        LogoutCommand,
        bool>
{
    private readonly IAuthService
        _authService;

    private readonly ICurrentUserService
        _currentUser;

    private readonly ICookieService
        _cookieService;

    public LogoutCommandHandler(
        IAuthService authService,
        ICurrentUserService currentUser,
        ICookieService cookieService)
    {
        _authService = authService;

        _currentUser = currentUser;

        _cookieService = cookieService;
    }

    public async Task<bool> Handle(
        LogoutCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
        {
            return false;
        }

        var result =
            await _authService.LogoutAsync(
                _currentUser.UserId.Value,
                ct);

        if (!result)
        {
            return false;
        }

        _cookieService.ClearAuthCookies();

        return true;
    }
}


