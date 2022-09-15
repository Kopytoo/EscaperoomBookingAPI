using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
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

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var bookingDetailsDtos = await _unitOfWork.BookingsDetails.GetAllBookingDetailsAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var bookingDetailsDto = await _unitOfWork.BookingsDetails.GetBookingDetailsByIdAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBySummaryId([FromRoute] Guid id)
    {
        var bookingDetailsDto = await _unitOfWork.BookingsDetails.GetBookingDetailsBySummaryIdAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] Guid summaryId, [FromForm] BookingDetailsDto bookingDetails)
    {
        if (ModelState.IsValid)
        {
            var newBookingDetails = await _unitOfWork.BookingsDetails.CreateBookingDetailsAsync(summaryId,
                bookingDetails.SelectedRoom, bookingDetails.VisitDate, bookingDetails.NumberOfPeople);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.Summaries.UpdateSummaryAsync(summaryId, newBookingDetails.Id, Guid.Empty);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetById", new { newBookingDetails.Id }, newBookingDetails);
        }

        return new JsonResult("Something Went Wrong") { StatusCode = 500 };
    }
}