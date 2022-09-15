using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

public interface ICustomerDetailsService
{
    Task<IEnumerable<CustomerDetailsDto>> GetAllCustomerDetailsAsync();
    Task<CustomerDetailsDto> GetCustomerDetailsByIdAsync(Guid id);
    Task<CustomerDetailsDto> GetCustomerDetailsBySummaryIdAsync(Guid id);
    Task<CustomerDetails> CreateCustomerDetailsAsync(Guid summaryId, CustomerDetailsDto customerDetails);
}