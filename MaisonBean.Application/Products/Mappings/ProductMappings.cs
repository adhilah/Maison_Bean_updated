using MaisonBean.Domain.Entities;

namespace MaisonBean.Application.Products.Queries;

public static class ProductMappings
{
    public static ProductDto ToDto(
        this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            IsActive = product.IsActive,
            IsBlocked = product.IsBlocked,
            Category = product.Category,
            Image = product.Image,
            BaseCalories = product.BaseCalories,
            HealthBenefits = product.HealthBenefits
        };
    }
}