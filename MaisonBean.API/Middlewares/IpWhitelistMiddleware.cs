using System.Net;

namespace MaisonBean.API.Middleware;

public class IpWhitelistMiddleware
{
    private readonly RequestDelegate _next;

    private readonly List<string> _whitelistedIps;

    public IpWhitelistMiddleware(
        RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;

        _whitelistedIps =
            configuration
                .GetSection("IpWhitelist")
                .Get<List<string>>()
            ?? new List<string>();
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        var requestIp =
            context.Connection
                .RemoteIpAddress?
                .ToString();

        if (!_whitelistedIps.Contains(requestIp))
        {
            context.Response.StatusCode = 403;

            await context.Response.WriteAsync(
                "Access Denied: IP not allowed");

            return;
        }

        await _next(context);
    }
}