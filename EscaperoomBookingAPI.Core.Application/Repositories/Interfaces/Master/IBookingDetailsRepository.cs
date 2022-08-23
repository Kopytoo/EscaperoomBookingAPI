using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

public interface IBookingDetailsRepository : IGenericRepository<BookingDetails, Guid>
{
}