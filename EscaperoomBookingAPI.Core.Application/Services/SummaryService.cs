using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

namespace EscaperoomBookingAPI.Core.Application.Services;

public class SummaryService : ISummaryService
{
    public readonly IUnitOfWork _unitOfWork;

    public SummaryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<SummaryDto>> GetAllSummariesAsync()
    {
        var summaries = await _unitOfWork.Summaries.GetAllAsync();

        var summaryDtos = summaries.Select(b => new SummaryDto()
        {
            Id = b.Id,
            Status = b.Status,
            CreationDate = b.CreationDate,
            Price = b.Price,
            BookingDetails = b.BookingDetails == null
                ? null
                : new BookingDetailsDto
                {
                    SelectedRoom = b.BookingDetails.SelectedRoom,
                    VisitDate = b.BookingDetails.VisitDate,
                    NumberOfPeople = b.BookingDetails.NumberOfPeople,
                },
            CustomerDetails = b.CustomerDetails == null
                ? null
                : new CustomerDetailsDto
                {
                    Name = b.CustomerDetails.Name,
                    Email = b.CustomerDetails.Email,
                    PhoneNumber = b.CustomerDetails.PhoneNumber,
                    OtherInfo = b.CustomerDetails.OtherInfo
                }
        });
        return summaryDtos;
    }

    public async Task<IEnumerable<SummaryDto>> GetSummariesByRoomAsync(Room room)
    {
        var summaries = await _unitOfWork.Summaries.GetByRoomAsync(room);

        var summaryDtos = summaries.Select(b => new SummaryDto()
        {
            Id = b.Id,
            Status = b.Status,
            CreationDate = b.CreationDate,
            Price = b.Price,
            BookingDetails = b.BookingDetails == null
                ? null
                : new BookingDetailsDto
                {
                    SelectedRoom = b.BookingDetails.SelectedRoom,
                    VisitDate = b.BookingDetails.VisitDate,
                    NumberOfPeople = b.BookingDetails.NumberOfPeople,
                },
            CustomerDetails = b.CustomerDetails == null
                ? null
                : new CustomerDetailsDto
                {
                    Name = b.CustomerDetails.Name,
                    Email = b.CustomerDetails.Email,
                    PhoneNumber = b.CustomerDetails.PhoneNumber,
                    OtherInfo = b.CustomerDetails.OtherInfo
                }
        });
        return summaryDtos;
    }

    public async Task<SummaryDto> GetSummaryByIdAsync(Guid id)
    {
        var summary = await _unitOfWork.Summaries.GetByIdAsync(id);

        var summaryDto = new SummaryDto()
        {
            Id = summary.Id,
            Status = summary.Status,
            CreationDate = summary.CreationDate,
            Price = summary.Price,
            BookingDetails = summary.BookingDetails == null
                ? null
                : new BookingDetailsDto
                {
                    SelectedRoom = summary.BookingDetails.SelectedRoom,
                    VisitDate = summary.BookingDetails.VisitDate,
                    NumberOfPeople = summary.BookingDetails.NumberOfPeople,
                },
            CustomerDetails = summary.CustomerDetails == null
                ? null
                : new CustomerDetailsDto
                {
                    Name = summary.CustomerDetails.Name,
                    Email = summary.CustomerDetails.Email,
                    PhoneNumber = summary.CustomerDetails.PhoneNumber,
                    OtherInfo = summary.CustomerDetails.OtherInfo
                }
        };
        return summaryDto;
    }

    public async Task<Summary> CreateSummaryAsync()
    {
        var newSummary = new Summary()
        {
            Id = Guid.NewGuid(),
            Status = BookingStatus.Pending,
            CreationDate = DateTime.Now,
            BookingVariant = Variant.Default,
            Price = 0,
            BookingDetails = null,
            CustomerDetails = null
        };

        await _unitOfWork.Summaries.AddAsync(newSummary);
        await _unitOfWork.SaveChangesAsync();

        return newSummary;
    }

    public async Task<Summary> UpdateSummaryAsync(Guid summaryId, Guid bookingDetailsId, Guid customerDetailsId)
    {
        var existingSummary = await _unitOfWork.Summaries.GetByIdAsync(summaryId);
        var bookingDetails = await _unitOfWork.BookingsDetails.GetByIdAsync(bookingDetailsId);
        var customerDetails = await _unitOfWork.CustomersDetails.GetByIdAsync(customerDetailsId);

        if (existingSummary == null)
            return null;

        if (bookingDetails != null)
        {
            existingSummary.BookingDetails = bookingDetails;
            existingSummary.BookingVariant =
                bookingDetails.VisitDate.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday or DayOfWeek.Sunday
                    ? Variant.Weekend
                    : Variant.Weekday;
            existingSummary.Price = existingSummary.BookingVariant is Variant.Weekend ? 200 : 170;
        }

        if (customerDetails != null)
            existingSummary.CustomerDetails = customerDetails;

        await _unitOfWork.SaveChangesAsync();
        
        return existingSummary;
    }

    public async Task<Summary> UpdateSummaryStatusAsync(Guid summaryId, BookingStatus status)
    {
        var summary = await _unitOfWork.Summaries.GetByIdAsync(summaryId);

        summary.Status = status;

        await _unitOfWork.SaveChangesAsync();

        return summary;
    }
}