using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

namespace EscaperoomBookingAPI.Core.Application.Services;

public class CustomerDetailsService : ICustomerDetailsService
{
    public readonly IUnitOfWork _unitOfWork;

    public CustomerDetailsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<CustomerDetailsDto>> GetAllCustomerDetailsAsync()
    {
        var customerDetails = await _unitOfWork.CustomersDetails.GetAllAsync();

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
        var customerDetails = await _unitOfWork.CustomersDetails.GetByIdAsync(id);

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
        var customerDetails = await _unitOfWork.CustomersDetails.GetBySummaryIdAsync(summaryId);

        var customerDetailsDto = new CustomerDetailsDto
        {
            Name = customerDetails.Name,
            Email = customerDetails.Email,
            PhoneNumber = customerDetails.PhoneNumber,
            OtherInfo = customerDetails.OtherInfo
        };
        return customerDetailsDto;
    }

    public async Task<CustomerDetails> CreateCustomerDetailsAsync(Guid summaryId, CustomerDetailsDto customerDetails)
    {
        var summary = await _unitOfWork.Summaries.GetByIdAsync(summaryId);

        var newCustomerDetails = new CustomerDetails
        {
            Id = Guid.NewGuid(),
            Name = customerDetails.Name,
            Email = customerDetails.Email,
            PhoneNumber = customerDetails.PhoneNumber,
            OtherInfo = customerDetails.OtherInfo,
            Summary = summary,
            SummaryReference = summary.Id
        };

        await _unitOfWork.CustomersDetails.AddAsync(newCustomerDetails);
        await _unitOfWork.SaveChangesAsync();

        return newCustomerDetails;
    }
}