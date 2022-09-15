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

    public async Task<IEnumerable<SummaryDto>> GetAllSummariesAsync()
    {
        var summaries = await GetAllAsync();

        var summaryDtos = summaries.Select(b => new SummaryDto()
        {
            Id = b.Id,
            Status = b.Status,
            CreationDate = b.CreationDate,
            Price = b.Price,
            BookingDetails = b.BookingDetails == null
                ? null
                : new BookingDetailsDto
                {
                    SelectedRoom = b.BookingDetails.SelectedRoom,
                    VisitDate = b.BookingDetails.VisitDate,
                    NumberOfPeople = b.BookingDetails.NumberOfPeople,
                },
            CustomerDetails = b.CustomerDetails == null
                ? null
                : new CustomerDetailsDto
                {
                    Name = b.CustomerDetails.Name,
                    Email = b.CustomerDetails.Email,
                    PhoneNumber = b.CustomerDetails.PhoneNumber,
                    OtherInfo = b.CustomerDetails.OtherInfo
                }
        });
        return summaryDtos;
    }

    public async Task<IEnumerable<SummaryDto>> GetSummariesByRoomAsync(Room room)
    {
        var summaries = await _dbSet
            .Include(s => s.BookingDetails)
            .Include(s => s.CustomerDetails)
            .Where(s => s.BookingDetails.SelectedRoom == room)
            .ToListAsync();

        var summaryDtos = summaries.Select(b => new SummaryDto()
        {
            Id = b.Id,
            Status = b.Status,
            CreationDate = b.CreationDate,
            Price = b.Price,
            BookingDetails = b.BookingDetails == null
                ? null
                : new BookingDetailsDto
                {
                    SelectedRoom = b.BookingDetails.SelectedRoom,
                    VisitDate = b.BookingDetails.VisitDate,
                    NumberOfPeople = b.BookingDetails.NumberOfPeople,
                },
            CustomerDetails = b.CustomerDetails == null
                ? null
                : new CustomerDetailsDto
                {
                    Name = b.CustomerDetails.Name,
                    Email = b.CustomerDetails.Email,
                    PhoneNumber = b.CustomerDetails.PhoneNumber,
                    OtherInfo = b.CustomerDetails.OtherInfo
                }
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
                : new BookingDetailsDto
                {
                    SelectedRoom = summary.BookingDetails.SelectedRoom,
                    VisitDate = summary.BookingDetails.VisitDate,
                    NumberOfPeople = summary.BookingDetails.NumberOfPeople,
                },
            CustomerDetails = summary.CustomerDetails == null
                ? null
                : new CustomerDetailsDto
                {
                    Name = summary.CustomerDetails.Name,
                    Email = summary.CustomerDetails.Email,
                    PhoneNumber = summary.CustomerDetails.PhoneNumber,
                    OtherInfo = summary.CustomerDetails.OtherInfo
                }
        };
        return summaryDto;
    }

    public async Task<Summary> CreateSummaryAsync()
    {
        var newSummary = new Summary()
        {
            Id = Guid.NewGuid(),
            Status = BookingStatus.Pending,
            CreationDate = DateTime.Now,
            BookingVariant = Variant.Default,
            Price = 0,
            BookingDetails = null,
            CustomerDetails = null
        };

        await AddAsync(newSummary);

        return newSummary;
    }

    public async Task<Summary> UpdateSummaryAsync(Guid summaryId, Guid bookingDetailsId, Guid customerDetailsId)
    {
        var existingSummary = await _dbSet.Where(x => x.Id == summaryId).FirstOrDefaultAsync();
        var bookingDetails = await _context.BookingDetails.Where(b => b.Id == bookingDetailsId).FirstOrDefaultAsync();
        var customerDetails =
            await _context.CustomerDetails.Where(c => c.Id == customerDetailsId).FirstOrDefaultAsync();

        if (existingSummary == null)
            return null;

        if (bookingDetails != null)
        {
            existingSummary.BookingDetails = bookingDetails;
            existingSummary.BookingVariant =
                bookingDetails.VisitDate.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday or DayOfWeek.Sunday
                    ? Variant.Weekend
                    : Variant.Weekday;
            existingSummary.Price = existingSummary.BookingVariant is Variant.Weekend ? 200 : 170;
        }

        if (customerDetails != null)
            existingSummary.CustomerDetails = customerDetails;

        return existingSummary;
    }

    public async Task<Summary> UpdateSummaryStatusAsync(Guid summaryId, BookingStatus status)
    {
        var summary = await _dbSet.Where(x => x.Id == summaryId).FirstOrDefaultAsync();

        summary.Status = status;

        return summary;
    }
}