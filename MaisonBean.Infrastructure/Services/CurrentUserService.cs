using MaisonBean.Application.Interfaces;

using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace MaisonBean.Infrastructure.Services;

public class CurrentUserService
    : ICurrentUserService
{
    private readonly IHttpContextAccessor
        _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor
            httpContextAccessor)
    {
        _httpContextAccessor =
            httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var id =
                _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue("id");

            if (int.TryParse(id, out var userId))
            {
                return userId;
            }

            return null;
        }
    }
}