using MaisonBean.API.Requests;
using MaisonBean.Application.Common;
using MaisonBean.Application.Products.Commands;
using MaisonBean.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaisonBean.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken ct)
        => Ok(
            await _mediator.Send(
                new GetAllProductsQuery(),
                ct
            )
        );

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken ct)
        => Ok(
            await _mediator.Send(
                new GetProductByIdQuery(id),
                ct
            )
        );

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string term,
        CancellationToken ct)
        => Ok(
            await _mediator.Send(
                new SearchProductsQuery(term),
                ct
            )
        );

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(
        string category,
        CancellationToken ct)
        => Ok(
            await _mediator.Send(
                new GetProductsByCategoryQuery(category),
                ct
            )
        );

    //[Authorize(Roles = "ADMIN")]
    //[HttpPost]
    //public async Task<IActionResult> Create(
    //    [FromForm] CreateProductCommand command,
    //    CancellationToken ct)
    //    => Ok(
    //        await _mediator.Send(
    //            command,
    //            ct
    //        )
    //    );


    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    public async Task<IActionResult> Create(
    [FromForm] CreateProductRequest request,
    CancellationToken ct)
    {
        var command =
            new CreateProductCommand
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                Category = request.Category,
                BaseCalories = request.BaseCalories,
                HealthBenefits = request.HealthBenefits,

                Image = new UploadImageDto
                {
                    Stream =
                        request.Image.OpenReadStream(),

                    FileName =
                        request.Image.FileName
                }
            };

        var result =
            await _mediator.Send(
                command,
                ct
            );

        return Ok(result);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromForm] UpdateProductCommand command,
        CancellationToken ct)
    {
        command.Id = id;

        await _mediator.Send(
            command,
            ct
        );

        return NoContent();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct)
    {
        await _mediator.Send(
            new DeleteProductCommand
            {
                Id = id
            },
            ct
        );

        return NoContent();
    }
}