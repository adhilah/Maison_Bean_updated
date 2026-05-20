using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;

using MediatR;

using System.ComponentModel.DataAnnotations;

namespace MaisonBean.Application.Addresses.Commands;

public class CreateAddressCommand
    : IRequest<object>
{
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

public class CreateAddressCommandHandler
    : IRequestHandler<CreateAddressCommand, object>
{
    private readonly IAddressRepository _repo;

    private readonly IUnitOfWork _uow;

    private readonly ICurrentUserService
        _currentUser;

    public CreateAddressCommandHandler(
        IAddressRepository repo,
        IUnitOfWork uow,
        ICurrentUserService currentUser)
    {
        _repo = repo;

        _uow = uow;

        _currentUser = currentUser;
    }

    public async Task<object> Handle(
    CreateAddressCommand request,
    CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
        {
            throw new UnauthorizedAccessException();
        }

        var userId =
            _currentUser.UserId.Value;

        var address = new Address
        {
            UserId = userId,

            DeliveryAddress =
                request.DeliveryAddress,

            City = request.City,

            Phone = request.Phone
        };

        await _repo.AddAsync(
            address,
            ct);

        await _uow.SaveChangesAsync(ct);

        return new
        {
            addressId = address.Id,

            deliveryAddress =
                address.DeliveryAddress,

            city = address.City,

            phone = address.Phone
        };
    }
}