using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MaisonBean.API.Attributes;

public class AdminIpWhitelistAttribute
    : ActionFilterAttribute
{
    private readonly List<string> _allowedIps =
        new()
        {
            "127.0.0.1",
            "::1",
            "192.168.1.17"
        };

    public override void OnActionExecuting(
        ActionExecutingContext context)
    {
        var requestIp =
            context.HttpContext
                .Connection
                .RemoteIpAddress?
                .ToString();

        if (!_allowedIps.Contains(requestIp))
        {
            context.Result =
                new ContentResult
                {
                    StatusCode = 403,

                    Content =
                        "Forbidden: IP not allowed"
                };

            return;
        }

        base.OnActionExecuting(context);
    }
}