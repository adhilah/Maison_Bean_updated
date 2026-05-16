//======================================
// GET ALL BEANS FOR ADMIN - QUERY
//======================================

using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MaisonBean.Application.BeanTypes.Queries;

public record GetAllBeansForAdminQuery
    : IRequest<List<BeanType>>;     

public class GetAllBeansForAdminQueryHandler
    : IRequestHandler<
        GetAllBeansForAdminQuery,
        List<BeanType>>
{
    private readonly IBeanTypeRepository _repo;

    public GetAllBeansForAdminQueryHandler(
        IBeanTypeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<BeanType>> Handle(
        GetAllBeansForAdminQuery request,
        CancellationToken cancellationToken)
    {
        return await _repo.GetAllForAdminAsync(
            cancellationToken);
    }
}