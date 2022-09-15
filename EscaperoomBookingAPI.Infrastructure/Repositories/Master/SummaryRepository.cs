using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using EscaperoomBookingAPI.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EscaperoomBookingAPI.Infrastructure.Repositories.Master;

public class SummaryRepository : GenericRepository<Summary, Guid>, ISummaryRepository
{
    public SummaryRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<Summary>> GetAllAsync()
    {
        return await _dbSet
            .Include(s => s.BookingDetails)
            .Include(s => s.CustomerDetails)
            .ToListAsync();
    }

    public override async Task<Summary> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(s => s.BookingDetails)
            .Include(s => s.CustomerDetails)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Summary>> GetByRoomAsync(Room room)
    {
        return await _dbSet
            .Include(s => s.BookingDetails)
            .Include(s => s.CustomerDetails)
            .Where(s => s.BookingDetails.SelectedRoom == room)
            .ToListAsync();
    }
}