using MaisonBean.Domain.Entities;

namespace MaisonBean.Application.Interfaces;
public interface IAddressRepository
{
    Task<List<Address>> GetByUserIdAsync(int userId, CancellationToken ct);
    Task<Address?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(Address address, CancellationToken ct);
    void Update(Address address);
    void Delete(Address address);
}