using MaisonBean.Application.Common;
using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MaisonBean.Application.Products.Commands;

public class CreateProductCommand
    : IRequest<int>
{
    [Required]
    public string Name { get; set; }
        = string.Empty;

    [Required]
    public string Description { get; set; }
        = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    [Required]
    public string Category { get; set; }
        = string.Empty;

    [Required]
    public UploadImageDto? Image { get; set; }

    public int BaseCalories { get; set; }

    [Required]
    public string HealthBenefits { get; set; }
        = string.Empty;
}

public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository
        _productRepo;

    private readonly IUnitOfWork
        _uow;

    private readonly IImageService
        _imageService;

    public CreateProductCommandHandler(
        IProductRepository productRepo,
        IUnitOfWork uow,
        IImageService imageService)
    {
        _productRepo = productRepo;

        _uow = uow;

        _imageService = imageService;
    }

    public async Task<int> Handle(
        CreateProductCommand request,
        CancellationToken ct)
    {
        var exists =
            await _productRepo
                .ExistsByNameAsync(
                    request.Name,
                    ct
                );

        if (exists)
        {
            throw new Exception(
                "Product already exists"
            );
        }

        await using var stream =
            request.Image.Stream;

        var uploadResult =
            await _imageService
                .UploadImageAsync(
                    stream,
                    request.Image.FileName,
                    ct
                );

        var product =
            Product.Create(
                request.Name,
                request.Description,
                request.Price,
                request.Stock,
                request.Category,
                uploadResult.ImageUrl,
                request.BaseCalories,
                request.HealthBenefits
            );

        await _productRepo
            .AddAsync(product);

        await _uow
            .SaveChangesAsync(ct);

        return product.Id;
    }
}