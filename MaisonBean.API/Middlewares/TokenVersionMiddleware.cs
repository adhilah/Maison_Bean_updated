using MaisonBean.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class TokenVersionMiddleware
{
    private readonly RequestDelegate _next;

    public TokenVersionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(
        HttpContext context,
        UserManager<AppUser> userManager)
    {
        if (
            context.User?.Identity == null ||
            !context.User.Identity.IsAuthenticated
        )
        {
            await _next(context);
            return;
        }

        var userId =
            context.User.FindFirstValue(
                "id"
            );

        var tokenVersion =
            context.User
                .FindFirst("tokenVersion")
                ?.Value;

        if (
            !string.IsNullOrEmpty(userId) &&
            tokenVersion != null
        )
        {
            var user =
                await userManager
                    .FindByIdAsync(userId);

            if (
                user == null ||
                user.TokenVersion.ToString()
                    != tokenVersion
            )
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync(
                    "Session expired. Please login again."
                );

                return;
            }
        }

        await _next(context);
    }
}
