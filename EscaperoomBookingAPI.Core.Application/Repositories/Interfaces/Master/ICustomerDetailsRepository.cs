using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

public interface ICustomerDetailsRepository : IGenericRepository<CustomerDetails, Guid>
{
    Task<CustomerDetails> GetBySummaryIdAsync(Guid id);
}