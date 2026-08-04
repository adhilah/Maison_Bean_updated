using MaisonBean.Application.Interfaces;
using MaisonBean.Domain.Entities;
using MaisonBean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaisonBean.Infrastructure.Repositories;

public class MilkOptionRepository : IMilkOptionRepository
{
    private readonly AppDbContext _db;

    public MilkOptionRepository(AppDbContext db)
    {
        _db = db;
    }

    // GET BY ID
    public async Task<MilkOption?> GetByIdAsync(
        int id,
        CancellationToken ct)
    {
        return await _db.MilkOptions
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }
    // GET ALL PUBLIC
    public async Task<List<MilkOption>> GetAllAsync(
        CancellationToken ct)
    {
        return await _db.MilkOptions
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(ct);
    }

    // GET ALL FOR ADMIN

    public async Task<List<MilkOption>>
GetAllForAdminAsync(
    CancellationToken ct)
    {
        return await _db.MilkOptions
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
        return await _db.MilkOptions
            .AnyAsync(
                x => x.Name.ToLower() == name.ToLower(),
                ct);
    }

    // ADD
    public async Task AddAsync(
        MilkOption entity,
        CancellationToken ct)
    {
        await _db.MilkOptions.AddAsync(entity, ct);
    }

    // UPDATE
    public void Update(MilkOption entity)
    {
        _db.MilkOptions.Update(entity);
    }

    // DELETE
    public void Delete(MilkOption entity)
    {
        _db.MilkOptions.Remove(entity);
    }
}