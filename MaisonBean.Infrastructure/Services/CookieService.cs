using MaisonBean.Application.Interfaces;

using Microsoft.AspNetCore.Http;

namespace MaisonBean.Infrastructure.Services;

public class CookieService
    : ICookieService
{
    private readonly IHttpContextAccessor
        _httpContextAccessor;

    public CookieService(
        IHttpContextAccessor
            httpContextAccessor)
    {
        _httpContextAccessor =
            httpContextAccessor;
    }

    public void SetAuthCookies(
        string accessToken,
        string refreshToken)
    {
        var response =
            _httpContextAccessor
                .HttpContext!
                .Response;

        var options =
            new CookieOptions
            {
                HttpOnly = true,

                Secure = true,

                SameSite =
                    SameSiteMode.None,

                Expires =
                    DateTime.UtcNow
                        .AddDays(7)
            };

        response.Cookies.Append(
            "accessToken",
            accessToken,
            options);

        response.Cookies.Append(
            "refreshToken",
            refreshToken,
            options);
    }

    public void ClearAuthCookies()
    {
        var response =
            _httpContextAccessor
                .HttpContext!
                .Response;

        response.Cookies.Delete(
            "accessToken");

        response.Cookies.Delete(
            "refreshToken");
    }

    public string? GetRefreshToken()
    {
        return _httpContextAccessor
            .HttpContext?
            .Request
            .Cookies["refreshToken"];
    }
}