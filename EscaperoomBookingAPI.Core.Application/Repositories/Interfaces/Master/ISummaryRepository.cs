using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;

namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

public interface ISummaryRepository : IGenericRepository<Summary, Guid>
{
    Task<IEnumerable<Summary>> GetByRoomAsync(Room room);
}