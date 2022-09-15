using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

public interface ICustomerDetailsRepository : IGenericRepository<CustomerDetails, Guid>
{
    Task<IEnumerable<CustomerDetailsDto>> GetAllCustomerDetailsAsync();
    Task<CustomerDetailsDto> GetCustomerDetailsByIdAsync(Guid id);
    Task<CustomerDetailsDto> GetCustomerDetailsBySummaryIdAsync(Guid id);
    Task<CustomerDetails> CreateCustomerDetailsAsync(Guid summaryId, string name, string email, int phoneNumber, string? otherInfo);

}