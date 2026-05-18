//using System.Security.Claims;
//using MaisonBean.Infrastructure.Persistence;
//using Microsoft.EntityFrameworkCore;

//public class BlockedUserMiddleware
//{
//    private readonly RequestDelegate _next;

//    public BlockedUserMiddleware(
//        RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(
//        HttpContext context,
//        AppDbContext db)
//    {
//        if (
//            context.User?.Identity == null ||
//            !context.User.Identity.IsAuthenticated
//        )
//        {
//            await _next(context);
//            return;
//        }

//        var userId =
//            context.User.FindFirstValue(
//               "id"
//            );

//        if (int.TryParse(userId, out var parsedUserId))
//        {
//            var user =
//                await db.Users
//                    .FirstOrDefaultAsync(
//                        u => u.Id == parsedUserId
//                    );

//            if (
//                user != null &&
//                user.IsBlocked
//            )
//            {
//                context.Response.StatusCode = 403;

//                await context.Response.WriteAsJsonAsync(
//                    new
//                    {
//                        message =
//                            "Your account is blocked"
//                    });

//                return;
//            }
//        }

//        await _next(context);
//    }
//}



using System.Security.Claims;
using MaisonBean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaisonBean.API.Middleware;
public class BlockedUserMiddleware
{
    private readonly RequestDelegate _next;

    public BlockedUserMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        AppDbContext db)
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

        if (int.TryParse(userId, out var parsedUserId))
        {
            var user =
                await db.Users
                    .FirstOrDefaultAsync(
                        u => u.Id == parsedUserId
                    );

            if (
                user != null &&
                user.IsBlocked
            )
            {
                context.Response.StatusCode = 403;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message =
                            "Your account is blocked"
                    });

                return;
            }
        }

        await _next(context);
    }
}