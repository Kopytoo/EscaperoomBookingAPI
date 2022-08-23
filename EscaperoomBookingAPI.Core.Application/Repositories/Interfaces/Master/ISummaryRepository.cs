using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

public interface ISummaryRepository : IGenericRepository<Summary, Guid>
{
}