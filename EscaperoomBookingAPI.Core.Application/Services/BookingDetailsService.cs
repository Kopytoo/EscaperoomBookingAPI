using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

namespace EscaperoomBookingAPI.Core.Application.Services;

public class BookingDetailsService : IBookingDetailsService
{
    public readonly IUnitOfWork _unitOfWork;

    public BookingDetailsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<BookingDetailsDto>> GetAllBookingDetailsAsync()
    {
        var bookingDetails = await _unitOfWork.BookingsDetails.GetAllAsync();

        var bookingDetailsDtos = bookingDetails.Select(b => new BookingDetailsDto
        {
            SelectedRoom = b.SelectedRoom,
            VisitDate = b.VisitDate,
            NumberOfPeople = b.NumberOfPeople
        });
        return bookingDetailsDtos;
    }

    public async Task<BookingDetailsDto> GetBookingDetailsByIdAsync(Guid id)
    {
        var bookingDetails = await _unitOfWork.BookingsDetails.GetByIdAsync(id);

        var bookingDetailsDto = new BookingDetailsDto
        {
            SelectedRoom = bookingDetails.SelectedRoom,
            VisitDate = bookingDetails.VisitDate,
            NumberOfPeople = bookingDetails.NumberOfPeople
        };
        return bookingDetailsDto;
    }

    public async Task<BookingDetailsDto> GetBookingDetailsBySummaryIdAsync(Guid id)
    {
        var bookingDetails = await _unitOfWork.BookingsDetails.GetBySummaryIdAsync(id);

        var bookingDetailsDto = new BookingDetailsDto
        {
            SelectedRoom = bookingDetails.SelectedRoom,
            VisitDate = bookingDetails.VisitDate,
            NumberOfPeople = bookingDetails.NumberOfPeople
        };
        return bookingDetailsDto;
    }

    public async Task<BookingDetails> CreateBookingDetailsAsync(Guid summaryId, BookingDetailsDto bookingDetails)
    {
        var summary = await _unitOfWork.Summaries.GetByIdAsync(summaryId);
        
        var newBookingDetails = new BookingDetails
        {
            Id = Guid.NewGuid(),
            SelectedRoom = bookingDetails.SelectedRoom,
            VisitDate = bookingDetails.VisitDate,
            NumberOfPeople = bookingDetails.NumberOfPeople,
            Summary = summary,
            SummaryReference = summary.Id
        };

        await _unitOfWork.BookingsDetails.AddAsync(newBookingDetails);
        await _unitOfWork.SaveChangesAsync();

        return newBookingDetails;
    }
}