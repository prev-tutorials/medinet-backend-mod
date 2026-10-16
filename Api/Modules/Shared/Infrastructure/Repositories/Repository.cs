using Api.Modules.Shared.Application.Contracts;
using Api.Modules.Shared.Domain;
using Api.Modules.Shared.Infrastructure.Persistence;

namespace Api.Modules.Shared.Infrastructure.Repositories;


public class Repository<T>(
    AppDbContext context
) : IRepository<T> where T : Entity
{
    private readonly AppDbContext _context = context;

    public async Task<T> AddAsync(T entity, CancellationToken ct)
    {
        await _context.Set<T>().AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken ct)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync(ct);
        return entity;
    }


    public async Task<T> DeleteAsync(T entity, CancellationToken ct)
    {
        entity.Delete();
        await _context.SaveChangesAsync(ct);
        return entity;
    }

}