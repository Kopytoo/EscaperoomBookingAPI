using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;

namespace EscaperoomBookingAPI.Core.Application.Services;

public class BookingDetailsService : IBookingDetailsService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingDetailsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<BookingDetailsDto>> GetAllBookingDetailsAsync()
    {
        var bookingDetails = await _unitOfWork.BookingsDetails.GetAll();

        var bookingDetailsDtos = bookingDetails.Select(b => new BookingDetailsDto
        {
            SelectedRoom = b.SelectedRoom,
            VisitDate = b.VisitDate,
            NumberOfPeople = b.NumberOfPeople
        });
        return bookingDetailsDtos;
    }

    public async Task<BookingDetailsDto> GetBookingDetailsAsync(Guid id)
    {
        var bookingDetails = await _unitOfWork.BookingsDetails.GetById(id);

        var bookingDetailsDto = new BookingDetailsDto
        {
            SelectedRoom = bookingDetails.SelectedRoom,
            VisitDate = bookingDetails.VisitDate,
            NumberOfPeople = bookingDetails.NumberOfPeople
        };
        return bookingDetailsDto;
    }

    public async Task<BookingDetails> CreateBookingDetailsAsync(BookingDetails bookingDetails)
    {
        // var bookingDetails = new BookingDetails
        // {
        //     
        // };
        
        await _unitOfWork.BookingsDetails.Add(bookingDetails);
        await _unitOfWork.SaveChangesAsync();
        
        return null;
    }
}