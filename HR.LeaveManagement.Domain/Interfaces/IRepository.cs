namespace HR.LeaveManagement.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> GetByIdAync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
}
