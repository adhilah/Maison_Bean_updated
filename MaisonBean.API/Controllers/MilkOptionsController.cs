using MaisonBean.API.Attributes;
using MaisonBean.Application.MilkOptions.Commands;
using MaisonBean.Application.MilkOptions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaisonBean.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MilkOptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MilkOptionsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken ct)
    {
        var result =
            await _mediator.Send(
                new GetMilkOptionsQuery(),
                ct
            );

        return Ok(result);
    }

    // GET ALL MILKS FOR ADMIN
    [AdminIpWhitelist]
    [Authorize(Roles = "ADMIN")]
    [HttpGet("all/ad")]
    public async Task<IActionResult> GetAllMilkOptions(
        CancellationToken ct)
    {
        var milks = await _mediator.Send(
            new GetAllMilkOptionsForAdminQuery(),
            ct);

        return Ok(milks);
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken ct)
    {
        var result =
            await _mediator.Send(
                new GetMilkOptionByIdQuery(id),
                ct
            );

        return Ok(result);
    }

    // CREATE
    [AdminIpWhitelist]
    [HttpPost("milk/ad")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create(
        [FromBody]
        CreateMilkOptionCommand command,
        CancellationToken ct)
    {
        var id =
            await _mediator.Send(
                command,
                ct
            );

        return Ok(new
        {
            success = true,

            message =
                "Milk option created successfully",

            id
        });
    }

    // UPDATE
    [AdminIpWhitelist]
    [HttpPut("{id}/ad")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody]
        UpdateMilkOptionCommand command,
        CancellationToken ct)
    {
        command.Id = id;

        await _mediator.Send(
            command,
            ct
        );

        return Ok(new
        {
            success = true,

            message =
                "Milk option updated successfully"
        });
    }

    // BLOCK / UNBLOCK
    [AdminIpWhitelist]
    [HttpPatch("{id}/block/ad")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> ToggleBlock(
        int id,
        CancellationToken ct)
    {
        var isBlocked =
            await _mediator.Send(
                new ToggleMilkOptionCommand(id),
                ct
            );

        return Ok(new
        {
            success = true,

            message = isBlocked
                ? "Milk option successfully blocked"
                : "Milk option successfully unblocked"
        });
    }

    // DELETE
    [AdminIpWhitelist]
    [HttpDelete("{id}/ad")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken ct)
    {
        await _mediator.Send(
            new DeleteMilkOptionCommand(id),
            ct
        );

        return Ok(new
        {
            success = true,

            message =
                "Milk option deleted successfully"
        });
    }
}