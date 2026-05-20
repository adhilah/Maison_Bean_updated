using Microsoft.AspNetCore.Http;

namespace MaisonBean.API.Requests;

public class CreateProductRequest
{
    public string Name { get; set; }
        = string.Empty;

    public string Description { get; set; }
        = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string Category { get; set; }
        = string.Empty;

    public IFormFile Image { get; set; }
        = default!;

    public int BaseCalories { get; set; }

    public string HealthBenefits { get; set; }
        = string.Empty;
}