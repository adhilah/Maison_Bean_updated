using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MaisonBean.Application.MilkOptions.Queries;

public record GetAllMilkOptionsForAdminQuery
    : IRequest<List<MilkOption>>;

public class GetAllMilkOptionsForAdminQueryHandler
    : IRequestHandler<
        GetAllMilkOptionsForAdminQuery,
        List<MilkOption>>
{
    private readonly IMilkOptionRepository _repo;

    public GetAllMilkOptionsForAdminQueryHandler(
        IMilkOptionRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<MilkOption>> Handle(
        GetAllMilkOptionsForAdminQuery request,
        CancellationToken cancellationToken)
    {
        return await _repo.GetAllForAdminAsync(
            cancellationToken);
    }
}