using Api.Modules.Shared.Application.Contracts;
using Api.Modules.Shared.Domain;
using Api.Modules.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Shared.Infrastructure.Repositories;


public class Repository<T>(
    AppDbContext context
) : IRepository<T> where T : Entity
{
    protected readonly AppDbContext _context = context;

    protected virtual IQueryable<T> Query() 
        => _context.Set<T>();


    public async Task<T[]> GetAllAsync(CancellationToken ct)
        => await Query().ToArrayAsync(ct);
        

    public async Task<T?> GetById(Guid id, CancellationToken ct)
        => await Query().FirstOrDefaultAsync(x => x.Id == id, ct);
    

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