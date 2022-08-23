using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

public interface IBookingDetailsService
{
    Task<IEnumerable<BookingDetailsDto>> GetAllBookingDetailsAsync();
    Task<BookingDetailsDto> GetBookingDetailsAsync(Guid id);
    Task<BookingDetails> CreateBookingDetailsAsync(BookingDetails bookingDetails);
}