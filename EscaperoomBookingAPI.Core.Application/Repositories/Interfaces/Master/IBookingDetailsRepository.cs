using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;

namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

public interface IBookingDetailsRepository : IGenericRepository<BookingDetails, Guid>
{
    Task<IEnumerable<BookingDetailsDto>> GetAllBookingDetailsAsync();
    Task<BookingDetailsDto> GetBookingDetailsByIdAsync(Guid id);
    Task<BookingDetailsDto> GetBookingDetailsBySummaryIdAsync(Guid id);
    Task<BookingDetails> CreateBookingDetailsAsync(Guid summaryId, Room selectedRoom, DateTime visitDate, int numberOfPeople);
}