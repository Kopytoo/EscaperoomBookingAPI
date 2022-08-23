using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

namespace EscaperoomBookingAPI.Core.Application.Services;

public class BookingDetailsService : IBookingDetailsService
{
    public Task<IEnumerable<BookingDetailsDto>> GetAllBookingDetailsAsync()
    {
        return null;
    }

    public Task<BookingDetailsDto> GetBookingDetailsAsync(Guid id)
    {
        return null;
    }

    public Task<BookingDetails> CreateBookingDetailsAsync(BookingDetails bookingDetails)
    {
        return null;
    }
}