using MaisonBean.Domain.Common;

namespace MaisonBean.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int StockQuantity { get; private set; }

    public bool IsActive { get; private set; } = true;

    public string Category { get; private set; } = string.Empty;

    public string Image { get; private set; } = string.Empty;

    //public string ImagePublicId { get; private set; } = string.Empty;

    public int BaseCalories { get; private set; }

    public string HealthBenefits { get; private set; } = string.Empty;

    public bool IsBlocked { get; private set; }

    private Product() { }

    public static Product Create(
        string name,
        string description,
        decimal price,
        int stock,
        string category,
        string image,
        int baseCalories,
        string healthBenefits)
    {
        return new Product
        {
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = stock,
            Category = category,
            Image = image,
            BaseCalories = baseCalories,
            HealthBenefits = healthBenefits
        };
    }

    public void UpdateDetails(
        string name,
        string description,
        decimal price,
        string category,
        string image,
        int baseCalories,
        string healthBenefits)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        Image = image;
        BaseCalories = baseCalories;
        HealthBenefits = healthBenefits;

        SetUpdatedAt();
    }

    public void ToggleBlock()
    {
        IsBlocked = !IsBlocked;
    }

    public void AddStock(int quantity)
    {
        StockQuantity += quantity;
    }

    public void ReduceStock(int quantity)
    {
        StockQuantity -= quantity;
    }
}