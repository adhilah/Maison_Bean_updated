using MaisonBean.Application.Interfaces;
using MediatR;

namespace MaisonBean.Application.Addresses.Commands;

public record DeleteAddressCommand(int Id)
    : IRequest<bool>; 



//handler
public class DeleteAddressHandler
    : IRequestHandler<
        DeleteAddressCommand,
        bool>
{
    private readonly IAddressRepository _repo;

    private readonly IUnitOfWork _uow;

    private readonly ICurrentUserService
        _currentUser;

    public DeleteAddressHandler(
        IAddressRepository repo,
        IUnitOfWork uow,
        ICurrentUserService currentUser)
    {
        _repo = repo;

        _uow = uow;

        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        DeleteAddressCommand request,
        CancellationToken ct)
    {
        var userId =
            _currentUser.UserId;

        if (string.IsNullOrEmpty(userId))
            return false;

        var address =
            await _repo.GetByIdAsync(
                request.Id,
                ct);

        if (address == null)
            return false;

        if (address.UserId != userId)
            return false;

        address.SoftDelete();

        _repo.Update(address);

        await _uow.SaveChangesAsync(ct);

        return true;
    }
}