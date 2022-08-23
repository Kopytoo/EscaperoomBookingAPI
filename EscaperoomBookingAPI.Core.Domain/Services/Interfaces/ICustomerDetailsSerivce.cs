using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

public interface ICustomerDetailsSerivce
{
    Task<IEnumerable<CustomerDetailsDto>> GetAllCustomerDetailsAsync();
    Task<CustomerDetailsDto> GetCustomerDetailsAsync(Guid id);
    Task<CustomerDetails> CreateCustomerDetailsAsync(CustomerDetails customerDetails);
}