namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;

public interface IGenericRepository<T, U> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(U id);
    Task<bool> AddAsync(T entity);
}