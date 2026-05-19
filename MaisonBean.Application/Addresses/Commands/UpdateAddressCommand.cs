using MaisonBean.Application.Interfaces;

using MediatR;

using System.ComponentModel.DataAnnotations;

namespace MaisonBean.Application.Addresses.Commands;

public class UpdateAddressCommand
    : IRequest<Unit>
{
    public int Id { get; set; }

    [Required]
    public string DeliveryAddress { get; set; }
        = string.Empty;

    [Required]
    public string City { get; set; }
        = string.Empty;

    [Required]
    public string Phone { get; set; }
        = string.Empty;
}

public class UpdateAddressCommandHandler
    : IRequestHandler<UpdateAddressCommand, Unit>
{
    private readonly IAddressRepository _repo;

    private readonly IUnitOfWork _uow;

    private readonly ICurrentUserService
        _currentUser;

    public UpdateAddressCommandHandler(
        IAddressRepository repo,
        IUnitOfWork uow,
        ICurrentUserService currentUser)
    {
        _repo = repo;

        _uow = uow;

        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(
        UpdateAddressCommand request,
        CancellationToken ct)
    {
        var userId =
            _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new UnauthorizedAccessException();
        }

        var address =
            await _repo.GetByIdAsync(
                request.Id,
                ct
            );

        if (address == null)
        {
            throw new Exception(
                "Address not found"
            );
        }

        if (address.UserId != userId)
        {
            throw new UnauthorizedAccessException();
        }

        address.DeliveryAddress =
            request.DeliveryAddress;

        address.City =
            request.City;

        address.Phone =
            request.Phone;

        _repo.Update(address);

        await _uow.SaveChangesAsync(ct);

        return Unit.Value;
    }
}