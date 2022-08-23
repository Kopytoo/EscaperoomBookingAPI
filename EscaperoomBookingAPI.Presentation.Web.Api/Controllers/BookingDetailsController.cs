using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscaperoomBookingAPI.Presentation.Web.Api.Controllers;

public class BookingDetailsController : Controller
{
    private readonly ILogger<BookingDetailsController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookingDetailsService _bookingDetailsService;

    public BookingDetailsController(ILogger<BookingDetailsController> logger, IUnitOfWork unitOfWork, IBookingDetailsService bookingDetailsService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _bookingDetailsService = bookingDetailsService;
    }
    
    // GET
    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookingsDtos = await _bookingDetailsService.GetAllBookingDetailsAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingsDtos);
    }
}