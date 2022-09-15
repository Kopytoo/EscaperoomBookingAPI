using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using EscaperoomBookingAPI.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EscaperoomBookingAPI.Infrastructure.Repositories.Master;

public class BookingDetailsRepository : GenericRepository<BookingDetails, Guid>, IBookingDetailsRepository
{
    private readonly ISummaryRepository _summaryRepository;
    
    public BookingDetailsRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<BookingDetailsDto>> GetAllBookingDetailsAsync()
    {
        var bookingDetails = await GetAllAsync();

        var bookingDetailsDtos = bookingDetails.Select(b => new BookingDetailsDto
        {
            SelectedRoom = b.SelectedRoom,
            VisitDate = b.VisitDate,
            NumberOfPeople = b.NumberOfPeople
        });
        return bookingDetailsDtos;
    }

    public async Task<BookingDetailsDto> GetBookingDetailsByIdAsync(Guid id)
    {
        var bookingDetails = await GetByIdAsync(id);

        var bookingDetailsDto = new BookingDetailsDto
        {
            SelectedRoom = bookingDetails.SelectedRoom,
            VisitDate = bookingDetails.VisitDate,
            NumberOfPeople = bookingDetails.NumberOfPeople
        };
        return bookingDetailsDto;
    }

    public async Task<BookingDetailsDto> GetBookingDetailsBySummaryIdAsync(Guid id)
    {
        var bookingDetails = await _dbSet.FirstOrDefaultAsync(b => b.SummaryReference == id);

        var bookingDetailsDto = new BookingDetailsDto
        {
            SelectedRoom = bookingDetails.SelectedRoom,
            VisitDate = bookingDetails.VisitDate,
            NumberOfPeople = bookingDetails.NumberOfPeople
        };
        return bookingDetailsDto;
    }

    public async Task<BookingDetails> CreateBookingDetailsAsync(Guid summaryId, Room selectedRoom, DateTime visitDate, int numberOfPeople)
    {
        var summary = await _context.Summary.Where(s => s.Id == summaryId).FirstOrDefaultAsync();
        
        var newBookingDetails = new BookingDetails
        {
            Id = Guid.NewGuid(),
            SelectedRoom = selectedRoom,
            VisitDate = visitDate,
            NumberOfPeople = numberOfPeople,
            Summary = summary,
            SummaryReference = summary.Id
        };

        await AddAsync(newBookingDetails);

        return newBookingDetails;
    }
}