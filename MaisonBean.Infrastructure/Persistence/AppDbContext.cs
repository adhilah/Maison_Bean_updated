using MaisonBean.Domain.Common;
using MaisonBean.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MaisonBean.Infrastructure.Persistence;

public class AppDbContext
    : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }
    // DB SETS
    public DbSet<Product> Products
    { get; set; }

    public DbSet<CartItem> CartItems
    { get; set; }

    public DbSet<Order> Orders
    { get; set; }

    public DbSet<OrderItem> OrderItems
    { get; set; }

    public DbSet<BeanType> BeanTypes
    { get; set; }

    public DbSet<MilkOption> MilkOptions
    { get; set; }

    public DbSet<Address> Addresses
    { get; set; }

    public DbSet<WishlistItem> WishlistItems
    { get; set; }

    // MODEL CONFIGURATION
    protected override void OnModelCreating(
        ModelBuilder builder
    )
    {
        base.OnModelCreating(builder);
        // ORDER RELATIONSHIP
        builder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // ADDRESS
        builder.Entity<Address>();
        // PRODUCT
        builder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        // GLOBAL FILTER
        builder.Entity<Product>()
            .HasQueryFilter(
                p => !p.IsBlocked
            );
        // BEAN TYPE
        builder.Entity<BeanType>()
            .HasQueryFilter(
                b => !b.IsBlocked
            );

        builder.Entity<BeanType>()
            .Property(b => b.PriceAdd)
            .HasPrecision(18, 2);
        // MILK OPTION
        builder.Entity<MilkOption>()
            .HasQueryFilter(
                m => !m.IsBlocked
            );

        builder.Entity<MilkOption>()
            .Property(m => m.PriceAdd)
            .HasPrecision(18, 2);

        // ORDER
        builder.Entity<Order>()
            .Property(o => o.Subtotal)
            .HasPrecision(18, 2);

        builder.Entity<Order>()
            .Property(o => o.Shipping)
            .HasPrecision(18, 2);

        builder.Entity<Order>()
            .Property(o => o.Total)
            .HasPrecision(18, 2);

        builder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();

        // ORDER ITEM
        builder.Entity<OrderItem>()
            .Property(o => o.UnitPrice)
            .HasPrecision(18, 2);

        builder.Entity<OrderItem>()
            .Property(o => o.BeanPriceAdd)
            .HasPrecision(18, 2);

        builder.Entity<OrderItem>()
            .Property(o => o.MilkPriceAdd)
            .HasPrecision(18, 2);

        // APPLY CONFIGURATIONS
        builder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly
        );

        // WISHLIST
        builder.Entity<WishlistItem>(entity =>
        {
            entity.HasKey(w => w.Id);

            entity.Property(w => w.UserId)
                  .IsRequired();

            entity.Property(w => w.ProductId)
                  .IsRequired();

            // PRODUCT RELATION
            entity.HasOne(w => w.Product)
                  .WithMany()
                  .HasForeignKey(w => w.ProductId)
                  .IsRequired(false);

            // USER RELATION
            entity.HasOne(w => w.User)
                  .WithMany(u => u.WishlistItems)
                  .HasForeignKey(w => w.UserId);

            // UNIQUE INDEX.HasQueryFilter(p => !p.IsBlocked)
            entity.HasIndex(
                w => new
                {
                    w.UserId,
                    w.ProductId
                }
            ).IsUnique();
        });
    }
    // SAVE CHANGES
    public override async Task<int> SaveChangesAsync(
        CancellationToken ct = default
    )
    {
        var entries =
            ChangeTracker
                .Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreatedAt();
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdatedAt();
            }
        }

        return await base.SaveChangesAsync(ct);
    }
}