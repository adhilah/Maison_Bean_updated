using MaisonBean.Application.Wishlist.DTOs;
using System;

public interface IWishlistRepository
{

    Task<WishlistItem?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(WishlistItem item, CancellationToken ct);
    void Remove(WishlistItem item);
    Task<IEnumerable<WishlistItem>> GetByUserIdAsync(int userId, CancellationToken ct);
    Task<WishlistItem?> GetByUserAndProductAsync(int userId, int productId, CancellationToken ct);
    Task<List<WishlistItemDto>> GetWishlistWithProducts(int userId, CancellationToken ct);
}