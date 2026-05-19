using MaisonBean.Application.Products.Commands;
using MaisonBean.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    {
        return Ok(
            await _mediator.Send(
                new GetAllProductsQuery(),
                ct
            )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken ct)
    {
        var result =
            await _mediator.Send(
                new GetProductByIdQuery(id),
                ct
            );

        return result == null
            ? NotFound()
            : Ok(result);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductCommand command)
    {
        var id =
            await _mediator.Send(command);

        return Ok(id);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return NoContent();
    }

    [Authorize(Roles = "ADMIN")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        await _mediator.Send(
            new DeleteProductCommand
            {
                Id = id
            });

        return NoContent();
    }
}