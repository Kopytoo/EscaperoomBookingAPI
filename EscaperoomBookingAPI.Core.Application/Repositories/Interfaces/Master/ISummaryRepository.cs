using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;

namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

public interface ISummaryRepository : IGenericRepository<Summary, Guid>
{
    Task<IEnumerable<SummaryDto>> GetAllSummariesAsync();
    Task<IEnumerable<SummaryDto>> GetSummariesByRoomAsync(Room room);
    Task<SummaryDto> GetSummaryByIdAsync(Guid id);
    Task<Summary> CreateSummaryAsync();
    Task<Summary> UpdateSummaryAsync(Guid summaryId, Guid bookingDetailsId, Guid customerDetailsId);
    Task<Summary> UpdateSummaryStatusAsync(Guid summaryId, BookingStatus status);
}