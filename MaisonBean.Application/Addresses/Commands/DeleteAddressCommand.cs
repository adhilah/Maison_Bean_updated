using MaisonBean.Application.Interfaces;

using MediatR;

namespace MaisonBean.Application.Addresses.Commands;

public class DeleteAddressCommand
    : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteAddressCommandHandler
    : IRequestHandler<
        DeleteAddressCommand,
        Unit>
{
    private readonly IAddressRepository
        _repo;

    private readonly IUnitOfWork
        _uow;

    private readonly ICurrentUserService
        _currentUser;

    public DeleteAddressCommandHandler(
        IAddressRepository repo,
        IUnitOfWork uow,
        ICurrentUserService currentUser)
    {
        _repo = repo;

        _uow = uow;

        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(
        DeleteAddressCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
        {
            throw new UnauthorizedAccessException();
        }

        var userId =
            _currentUser.UserId.Value;

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

        _repo.Delete(address);

        await _uow.SaveChangesAsync(ct);

        return Unit.Value;
    }
}