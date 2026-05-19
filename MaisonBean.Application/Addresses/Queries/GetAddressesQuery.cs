using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;

using MediatR;

namespace MaisonBean.Application.Addresses.Queries;

public class GetAddressesQuery
    : IRequest<List<Address>>
{
}

public class GetAddressesQueryHandler
    : IRequestHandler<
        GetAddressesQuery,
        List<Address>>
{
    private readonly IAddressRepository _repo;

    private readonly ICurrentUserService
        _currentUser;

    public GetAddressesQueryHandler(
        IAddressRepository repo,
        ICurrentUserService currentUser)
    {
        _repo = repo;

        _currentUser = currentUser;
    }

    public async Task<List<Address>> Handle(
        GetAddressesQuery request,
        CancellationToken ct)
    {
        var userId =
            _currentUser.UserId;

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new UnauthorizedAccessException();
        }

        return await _repo.GetByUserIdAsync(
            userId,
            ct
        );
    }
}