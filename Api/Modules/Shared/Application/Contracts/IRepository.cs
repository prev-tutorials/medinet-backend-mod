namespace Api.Modules.Shared.Application.Contracts;

public interface IRepository<T>
{
    Task<T> AddAsync(T entity, CancellationToken ct);
    Task<T> UpdateAsync(T entity, CancellationToken ct);
    Task<T> DeleteAsync(T entity, CancellationToken ct);
}