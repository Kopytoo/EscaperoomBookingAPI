namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;

public interface IGenericRepository<T, U> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(U id);
    Task<bool> Add(T entity);
}