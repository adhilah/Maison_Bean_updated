using MaisonBean.Domain.Entities;

namespace MaisonBean.Application.Interfaces;

public interface IBeanTypeRepository
{
    Task<BeanType?> GetByIdAsync(
        int id,
        CancellationToken ct);

    Task<List<BeanType>> GetAllAsync(
        CancellationToken ct);

    Task<List<BeanType>> GetAllForAdminAsync(
    CancellationToken ct);

    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken ct);

    Task AddAsync(
        BeanType entity,
        CancellationToken ct);

    void Update(BeanType entity);

    void Delete(BeanType entity);
}