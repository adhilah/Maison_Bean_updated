using MaisonBean.Application.Addresses.Commands;
using MaisonBean.Application.Addresses.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaisonBean.API.Controllers;

[Authorize]
[ApiController]
[Authorize(Roles = "CUSTOMER")]
[Route("api/address")]
public class AddressController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressController(
        IMediator mediator)
    {
        _mediator = mediator;
    }


    // ADD ADDRESS
    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] CreateAddressCommand command,
        CancellationToken ct)
    {
        var result =
            await _mediator.Send(
                command,
                ct
            );

        return Ok(result);
    }


    // GET USER ADDRESSES
    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken ct)
    {
        var result =
            await _mediator.Send(
                new GetAddressesQuery(),
                ct
            );

        return Ok(result);
    }

    // UPDATE ADDRESS
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateAddressCommand command,
        CancellationToken ct)
    {
        command.Id = id;

        await _mediator.Send(
            command,
            ct
        );

        return Ok(new
        {
            message =
                "Address updated successfully"
        });
    }

    // DELETE ADDRESS
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
    int id,
    CancellationToken ct)
    {
        await _mediator.Send(
            new DeleteAddressCommand
            {
                Id = id
            },
            ct
        );

        return NoContent();
    }
}