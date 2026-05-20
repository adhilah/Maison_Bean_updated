using MaisonBean.Application.Common;
using MaisonBean.Application.Interfaces;
using MediatR;

using System.ComponentModel.DataAnnotations;

namespace MaisonBean.Application.Products.Commands;

public class UpdateProductCommand
    : IRequest<Unit>
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
        = string.Empty;

    [Required]
    public string Description { get; set; }
        = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    [Required]
    public string Category { get; set; }
        = string.Empty;

    public UploadImageDto? Image { get; set; }

    public int BaseCalories { get; set; }

    [Required]
    public string HealthBenefits { get; set; }
        = string.Empty;
}

public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository
        _repo;

    private readonly IUnitOfWork
        _uow;

    private readonly IImageService
        _imageService;

    public UpdateProductCommandHandler(
        IProductRepository repo,
        IUnitOfWork uow,
        IImageService imageService)
    {
        _repo = repo;

        _uow = uow;

        _imageService = imageService;
    }

    public async Task<Unit> Handle(
        UpdateProductCommand req,
        CancellationToken ct)
    {
        var product =
            await _repo.GetByIdAsync(
                req.Id,
                ct
            );

        if (product == null)
        {
            throw new Exception(
                "Product not found"
            );
        }

        var imageUrl =
            product.Image;

        if (req.Image != null)
        {
            await using var stream =
                req.Image.Stream;

            var uploadResult =
                await _imageService
                    .UploadImageAsync(
                        stream,
                        req.Image.FileName,
                        ct
                    );

            imageUrl =
                uploadResult.ImageUrl;
        }

        product.UpdateDetails(
            req.Name,
            req.Description,
            req.Price,
            req.Category,
            imageUrl,
            req.BaseCalories,
            req.HealthBenefits
        );

        _repo.Update(product);

        await _uow.SaveChangesAsync(ct);

        return Unit.Value;
    }
}