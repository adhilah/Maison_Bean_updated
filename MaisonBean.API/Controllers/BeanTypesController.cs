using MaisonBean.API.Attributes;
using MaisonBean.Application.BeanTypes.Commands;
using MaisonBean.Application.BeanTypes.Queries;
using MediatR;
//using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BeanTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BeanTypesController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    // ======================================
    // GET ALL BEANS
    // ======================================

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(
            await _mediator.Send(
                new GetBeanTypesQuery()
            )
        );
    }


    //======================================
    // GET ALL BEANS FOR ADMIN
    //======================================


    [AdminIpWhitelist]
    [Authorize(Roles = "ADMIN")]
    [HttpGet("all/ad")]
    public async Task<IActionResult> GetAllBeans(
        CancellationToken ct)
    {
        var beans = await _mediator.Send(
            new GetAllBeansForAdminQuery(),
            ct);

        return Ok(beans);
    }

    // ======================================
    // CREATE BEAN
    // ======================================


    [AdminIpWhitelist]
    [HttpPost("bean/ad")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Create(
        CreateBeanTypeCommand command)
    {
        var id =
            await _mediator.Send(command);

        return Ok(new
        {
            message =
                "Bean type added successfully",

            id
        });
    }

    // ======================================
    // UPDATE BEAN
    // ======================================


    [AdminIpWhitelist]
    [HttpPut("{id}/update/ad")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody]
        UpdateBeanTypeCommand command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return Ok(new
        {
            message =
                "Bean type updated successfully"
        });
    }

    // ======================================
    // BLOCK / UNBLOCK BEAN
    // ======================================


    [AdminIpWhitelist]
    [HttpPatch("{id}/block/ad")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Toggle(
        int id)
    {
        var isBlocked =
            await _mediator.Send(
                new ToggleBeanTypeCommand(id)
            );

        return Ok(new
        {
            message = isBlocked
                ? "Bean type successfully blocked"
                : "Bean type successfully unblocked"
        });
    }

    // ======================================
    // DELETE BEAN
    // ======================================


    [AdminIpWhitelist]
    [HttpDelete("{id}/ad")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Delete(
        int id)
    {
        await _mediator.Send(
            new DeleteBeanTypeCommand(id)
        );

        return Ok(new
        {
            message =
                "Bean type deleted successfully"
        });
    }
}