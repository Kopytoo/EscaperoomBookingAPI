using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;

namespace EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

public interface ISummaryService
{
    Task<IEnumerable<SummaryDto>> GetAllSummariesAsync();
    Task<IEnumerable<SummaryDto>> GetSummariesByRoomAsync(Room room);
    Task<SummaryDto> GetSummaryByIdAsync(Guid id);
    Task<Summary> CreateSummaryAsync();
    Task<Summary> UpdateSummaryAsync(Guid summaryId, Guid bookingDetailsId, Guid customerDetailsId);
    Task<Summary> UpdateSummaryStatusAsync(Guid summaryId, BookingStatus status);
}