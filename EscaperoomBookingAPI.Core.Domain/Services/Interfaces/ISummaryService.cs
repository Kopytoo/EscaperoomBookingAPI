using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;

namespace EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

public interface ISummaryService
{
    Task<IEnumerable<SummaryDto>> GetAllCustomerDetailsAsync();
    Task<SummaryDto> GetCustomerDetailsAsync(Guid id);
    Task<Summary> CreateCustomerDetailsAsync(Summary summary);
}