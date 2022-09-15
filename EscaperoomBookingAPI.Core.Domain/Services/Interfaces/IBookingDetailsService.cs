using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;

namespace EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

public interface IBookingDetailsService
{
    Task<IEnumerable<BookingDetailsDto>> GetAllBookingDetailsAsync();
    Task<BookingDetailsDto> GetBookingDetailsByIdAsync(Guid id);
    Task<BookingDetailsDto> GetBookingDetailsBySummaryIdAsync(Guid id);
    Task<BookingDetails> CreateBookingDetailsAsync(Guid summaryId, BookingDetailsDto bookingDetails);
}