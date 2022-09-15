using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using EscaperoomBookingAPI.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EscaperoomBookingAPI.Infrastructure.Repositories.Master;

public class CustomerDetailsRepository : GenericRepository<CustomerDetails, Guid>, ICustomerDetailsRepository
{
    private readonly ISummaryRepository _summaryRepository;

    public CustomerDetailsRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<CustomerDetailsDto>> GetAllCustomerDetailsAsync()
    {
        var customerDetails = await GetAllAsync();

        var customerDetailsDtos = customerDetails.Select(c => new CustomerDetailsDto
        {
            Name = c.Name,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            OtherInfo = c.OtherInfo
        });
        return customerDetailsDtos;
    }
    
    public async Task<CustomerDetailsDto> GetCustomerDetailsByIdAsync(Guid id)
    {
        var customerDetails = await GetByIdAsync(id);

        var customerDetailsDto = new CustomerDetailsDto
        {
            Name = customerDetails.Name,
            Email = customerDetails.Email,
            PhoneNumber = customerDetails.PhoneNumber,
            OtherInfo = customerDetails.OtherInfo
        };
        return customerDetailsDto;
    }

    public async Task<CustomerDetailsDto> GetCustomerDetailsBySummaryIdAsync(Guid summaryId)
    {
        var customerDetails = await _dbSet.FirstOrDefaultAsync(c => c.SummaryReference == summaryId);

        var customerDetailsDto = new CustomerDetailsDto
        {
            Name = customerDetails.Name,
            Email = customerDetails.Email,
            PhoneNumber = customerDetails.PhoneNumber,
            OtherInfo = customerDetails.OtherInfo
        };
        return customerDetailsDto;
    }

    public async Task<CustomerDetails> CreateCustomerDetailsAsync(Guid summaryId, string name, string email, int phoneNumber, string? otherInfo)
    {
        var summary = await _context.Summary.Where(s => s.Id == summaryId).FirstOrDefaultAsync();
        
        var newCustomerDetails = new CustomerDetails
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            OtherInfo = otherInfo,
            Summary = summary,
            SummaryReference = summary.Id
        };

        await AddAsync(newCustomerDetails);

        return newCustomerDetails;
    }
}