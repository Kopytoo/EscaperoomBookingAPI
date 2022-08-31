using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EscaperoomBookingAPI.Presentation.Web.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BookingDetailsController : Controller
{
    private readonly ILogger<BookingDetailsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public BookingDetailsController(ILogger<BookingDetailsController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    // GET
    [HttpGet]
    // [ProducesResponseType()]
    public async Task<IActionResult> GetAll()
    {
        var bookingDetailsDtos = await _unitOfWork.BookingsDetails.GetAllBookingDetailsAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDtos);
    }
    
    // GET
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bookingDetailsDto = await _unitOfWork.BookingsDetails.GetBookingDetailsByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDto);
    }

    // GET
    [HttpGet]
    public async Task<IActionResult> GetBySummaryId(Guid id)
    {
        var bookingDetailsDto = await _unitOfWork.BookingsDetails.GetBookingDetailsBySummaryIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDto);
    }

    // POST
    [HttpPost]
    public async Task<IActionResult> Create(Guid summaryId, Room selectedRoom, DateTime visitDate, int numberOfPeople)
    {
        if (ModelState.IsValid)
        {
            var newBookingDetails = await _unitOfWork.BookingsDetails.CreateBookingDetailsAsync(summaryId, selectedRoom, visitDate, numberOfPeople);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.Summaries.UpdateSummaryAsync(summaryId, newBookingDetails.Id, Guid.Empty);

            return CreatedAtAction("GetById", new { newBookingDetails.Id }, newBookingDetails);
        }

        return new JsonResult("Something Went Wrong") { StatusCode = 500 };
    }
}