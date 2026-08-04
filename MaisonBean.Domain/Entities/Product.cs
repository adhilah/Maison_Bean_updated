using MaisonBean.Domain.Common;

namespace MaisonBean.Domain.Entities;

public class Product : BaseEntity
{
    public string Name
    {
        get;
        private set;
    } = string.Empty;

    public string Description
    {
        get;
        private set;
    } = string.Empty;

    public decimal Price
    {
        get;
        private set;
    }

    public int StockQuantity
    {
        get;
        private set;
    }

    public bool IsActive
    {
        get;
        private set;
    } = true;

    public string Category
    {
        get;
        private set;
    } = string.Empty;

    public string Image
    {
        get;
        private set;
    } = string.Empty;

    public int BaseCalories
    {
        get;
        private set;
    }

    public string HealthBenefits
    {
        get;
        private set;
    } = string.Empty;

    public bool IsBlocked
    {
        get;
        private set;
    }

    private Product()
    {
    }


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
        // VALIDATIONS
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Product name is required."
            );
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException(
                "Description is required."
            );
        }

        if (price <= 0)
        {
            throw new ArgumentException(
                "Price must be greater than zero."
            );
        }

        if (stock < 0)
        {
            throw new ArgumentException(
                "Stock cannot be negative."
            );
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException(
                "Category is required."
            );
        }

        if (string.IsNullOrWhiteSpace(image))
        {
            throw new ArgumentException(
                "Image is required."
            );
        }

        if (baseCalories < 0)
        {
            throw new ArgumentException(
                "Calories cannot be negative."
            );
        }

        if (string.IsNullOrWhiteSpace(healthBenefits))
        {
            throw new ArgumentException(
                "Health benefits are required."
            );
        }

        // CREATE PRODUCT
        return new Product
        {
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = stock,
            Category = category,
            Image = image,
            BaseCalories = baseCalories,
            HealthBenefits = healthBenefits,
            IsActive = true,
            IsBlocked = false
        };
    }

    // UPDATE DETAILS
    public void UpdateDetails(
        string name,
        string description,
        decimal price,
        string category,
        string image,
        int baseCalories,
        string healthBenefits)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Product name is required."
            );
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException(
                "Description is required."
            );
        }

        if (price <= 0)
        {
            throw new ArgumentException(
                "Price must be greater than zero."
            );
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException(
                "Category is required."
            );
        }

        if (string.IsNullOrWhiteSpace(image))
        {
            throw new ArgumentException(
                "Image is required."
            );
        }

        if (baseCalories < 0)
        {
            throw new ArgumentException(
                "Calories cannot be negative."
            );
        }

        if (string.IsNullOrWhiteSpace(healthBenefits))
        {
            throw new ArgumentException(
                "Health benefits are required."
            );
        }

        Name = name;
        Description = description;
        Price = price;
        Category = category;
        Image = image;
        BaseCalories = baseCalories;
        HealthBenefits = healthBenefits;

        SetUpdatedAt();
    }

    // BLOCK / UNBLOCK

    public void ToggleBlock()
    {
        IsBlocked = !IsBlocked;

        SetUpdatedAt();
    }

    // ACTIVATE / DEACTIVATE
    public void Activate()
    {
        IsActive = true;

        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;

        SetUpdatedAt();
    }


    //ADD STOCK
    public void AddStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException(
                "Quantity must be greater than zero."
            );
        }

        StockQuantity += quantity;

        SetUpdatedAt();
    }

    // REDUCE STOCK
    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException(
                "Quantity must be greater than zero."
            );
        }

        if (quantity > StockQuantity)
        {
            throw new InvalidOperationException(
                "Insufficient stock."
            );
        }

        StockQuantity -= quantity;

        SetUpdatedAt();
    }
}