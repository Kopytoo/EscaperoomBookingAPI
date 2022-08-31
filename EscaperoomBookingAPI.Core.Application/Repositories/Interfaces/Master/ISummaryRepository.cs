using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

public interface ISummaryRepository : IGenericRepository<Summary, Guid>
{
    Task<IEnumerable<SummaryDto>> GetAllSummariesAsync();
    Task<SummaryDto> GetSummaryByIdAsync(Guid id);
    Task<Summary> CreateSummaryAsync();
    Task<Summary> UpdateSummaryAsync(Guid summaryId, Guid bookingDetailsId, Guid customerDetailsId);
}