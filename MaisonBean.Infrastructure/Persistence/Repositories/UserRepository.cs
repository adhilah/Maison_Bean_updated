using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;
using MaisonBean.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace MaisonBean.Infrastructure.Persistence.Repositories;

public class UserRepository
    : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(
        AppDbContext db
    )
    {
        _db = db;
    }

    public Task<AppUser?> GetByIdAsync(
        int id,
        CancellationToken ct = default
    ) =>
        _db.Users.FirstOrDefaultAsync(
            u => u.Id == id,
            ct
        );

    public Task<AppUser?> GetByEmailAsync(
        string email,
        CancellationToken ct = default
    ) =>
        _db.Users.FirstOrDefaultAsync(
            u => u.Email == email,
            ct
        );

    // GET USER WITH RELATIONS
    //==============================
    //public async Task<AppUser?>
    //    GetUserWithRelationsAsync(
    //        int id,
    //        CancellationToken ct = default
    //    )
    //{
    //    return await _db.Users

    //        .Include(x => x.Orders)

    //        .Include(x => x.CartItems)

    //        .Include(x => x.WishlistItems)

    //        .Include(x => x.Addresses)

    //        .FirstOrDefaultAsync(
    //            x => x.Id == id,
    //            ct
    //        );
    //}
}