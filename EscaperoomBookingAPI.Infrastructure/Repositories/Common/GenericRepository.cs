using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EscaperoomBookingAPI.Infrastructure.Repositories.Common;

public class GenericRepository<T, U> : IGenericRepository<T, U> where T : class
{
    internal readonly ApplicationDbContext _context;
    internal readonly DbSet<T> _dbSet;
    private readonly ILogger _logger;

    public GenericRepository(ApplicationDbContext context, ILogger logger)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _logger = logger;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(U id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }
}