using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;
using MediatR;

namespace MaisonBean.Application.Wishlist.Commands;

public class ToggleWishlistHandler
    : IRequestHandler<
        ToggleWishlistCommand,
        WishlistResult>
{
    private readonly IWishlistRepository
        _wishlist;

    private readonly IProductRepository
        _products;

    private readonly IUnitOfWork
        _uow;

    public ToggleWishlistHandler(
        IWishlistRepository wishlist,
        IProductRepository products,
        IUnitOfWork uow)
    {
        _wishlist = wishlist;

        _products = products;

        _uow = uow;
    }

    public async Task<WishlistResult> Handle(
        ToggleWishlistCommand request,
        CancellationToken ct)
    {
        var existing =
            await _wishlist
                .GetByUserAndProductAsync(
                    request.UserId,
                    request.ProductId,
                    ct
                );

        // =====================================
        // REMOVE IF EXISTS
        // =====================================

        if (existing != null)
        {
            _wishlist.Remove(existing);

            await _uow.SaveChangesAsync(ct);

            return new WishlistResult
            {
                IsAdded = false,

                Message =
                    "Removed from wishlist"
            };
        }

        // =====================================
        // GET PRODUCT
        // =====================================

        var product =
            await _products
                .GetByIdAsync(
                    request.ProductId,
                    ct
                );

        if (product == null)
        {
            throw new Exception(
                "Product not found"
            );
        }

        // =====================================
        // CREATE WISHLIST ITEM
        // =====================================

        var item = new WishlistItem
        {
            UserId = request.UserId,
            ProductId = request.ProductId
        };

        // =====================================
        // SAVE
        // =====================================

        await _wishlist.AddAsync(
            item,
            ct
        );

        await _uow.SaveChangesAsync(ct);

        return new WishlistResult
        {
            IsAdded = true,

            Message =
                "Successfully added to wishlist"
        };
    }
}