using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;
using MaisonBean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaisonBean.Infrastructure.Repositories;

public class BeanTypeRepository : IBeanTypeRepository
{
    private readonly AppDbContext _db;

    public BeanTypeRepository(AppDbContext db)
    {
        _db = db;
    }

    // GET BY ID
    public async Task<BeanType?> GetByIdAsync(
        int id,
        CancellationToken ct)
    {
        return await _db.BeanTypes
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    // GET ALL PUBLIC
    public async Task<List<BeanType>> GetAllAsync(
        CancellationToken ct)
    {
        return await _db.BeanTypes
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
    }

    // GET ALL FOR ADMIN

    public async Task<List<BeanType>>
 GetAllForAdminAsync(
     CancellationToken ct)
    {
        return await _db.BeanTypes
            .IgnoreQueryFilters()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
    }
    // EXISTS BY NAME

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken ct)
    {
        return await _db.BeanTypes
            .AnyAsync(
                x => x.Name.ToLower() == name.ToLower(),
                ct);
    }

    // ADD
    public async Task AddAsync(
        BeanType entity,
        CancellationToken ct)
    {
        await _db.BeanTypes.AddAsync(entity, ct);
    }

    // UPDATE
    public void Update(BeanType entity)
    {
        _db.BeanTypes.Update(entity);
    }

    // DELETE
    public void Delete(BeanType entity)
    {
        _db.BeanTypes.Remove(entity);
    }
}