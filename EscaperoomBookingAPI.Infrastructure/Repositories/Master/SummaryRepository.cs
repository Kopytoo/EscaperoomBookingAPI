using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
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

    public async Task<IEnumerable<SummaryDto>> GetAllSummariesAsync()
    {
        var summaries = await GetAllAsync();

        var summaryDtos = summaries.Select(b => new SummaryDto()
        {
            Id = b.Id,
            Status = b.Status,
            CreationDate = b.CreationDate,
            Price = b.Price
        });
        return summaryDtos;
    }

    public async Task<SummaryDto> GetSummaryByIdAsync(Guid id)
    {
        var summary = await GetByIdAsync(id);

        var summaryDto = new SummaryDto()
        {
            Id = summary.Id,
            Status = summary.Status,
            CreationDate = summary.CreationDate,
            Price = summary.Price,
            BookingDetails = summary.BookingDetails == null
                ? null
                : new BookingDetailsDto()
                {
                    SelectedRoom = summary.BookingDetails.SelectedRoom,
                    VisitDate = summary.BookingDetails.VisitDate,
                    NumberOfPeople = summary.BookingDetails.NumberOfPeople
                }
        };
        return summaryDto;
    }

    public async Task<Summary> CreateSummaryAsync()
    {
        var newSummary = new Summary()
        {
            Id = Guid.NewGuid(),
            Status = 0,
            CreationDate = DateTime.Now,
            BookingVariant = 0,
            Price = 0,
            BookingDetails = null,
            CustomerDetails = null
        };

        await AddAsync(newSummary);

        return newSummary;
    }

    // TODO - Update nie działa
    public async Task<Summary> UpdateSummaryAsync(Guid summaryId, Guid bookingDetailsId, Guid customerDetailsId)
    {
        var existingSummary = await _dbSet.Where(x => x.Id == summaryId).FirstOrDefaultAsync();
        var bookingDetails = await _context.BookingDetails.Where(b => b.Id == bookingDetailsId).FirstOrDefaultAsync();
        var customerDetails =
            await _context.CustomerDetails.Where(c => c.Id == customerDetailsId).FirstOrDefaultAsync();

        if (existingSummary == null)
            return null;

        if (bookingDetails != null)
            existingSummary.BookingDetails = bookingDetails;

        if (customerDetails != null)
            existingSummary.CustomerDetails = customerDetails;

        return existingSummary;
    }
}