namespace MaisonBean.Application.Interfaces;

public interface ICookieService
{
    void SetAuthCookies(
        string accessToken,
        string refreshToken);

    void ClearAuthCookies();

    string? GetRefreshToken();
}