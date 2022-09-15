using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscaperoomBookingAPI.Presentation.Web.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BookingDetailsController : Controller
{
    private readonly ILogger<SummaryController> _logger;
    private readonly ISummaryService _summaryService;
    private readonly IBookingDetailsService _bookingDetailsService;

    public BookingDetailsController(ILogger<SummaryController> logger, ISummaryService summaryService, IBookingDetailsService bookingDetailsService)
    {
        _logger = logger;
        _summaryService = summaryService;
        _bookingDetailsService = bookingDetailsService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var bookingDetailsDtos = await _bookingDetailsService.GetAllBookingDetailsAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var bookingDetailsDto = await _bookingDetailsService.GetBookingDetailsByIdAsync(id);

        if (bookingDetailsDto == null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBySummaryId([FromRoute] Guid id)
    {
        var bookingDetailsDto = await _bookingDetailsService.GetBookingDetailsBySummaryIdAsync(id);
        
        if (bookingDetailsDto == null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(bookingDetailsDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] Guid summaryId, [FromForm] BookingDetailsDto bookingDetails)
    {
        if (_summaryService.GetSummaryByIdAsync(summaryId) == null)
            return NotFound();
        
        if (!ModelState.IsValid)
            return BadRequest();
        
        var newBookingDetails = await _bookingDetailsService.CreateBookingDetailsAsync(summaryId, bookingDetails);
        await _summaryService.UpdateSummaryAsync(summaryId, newBookingDetails.Id, Guid.Empty);

        return CreatedAtAction("GetById", new { newBookingDetails.Id }, newBookingDetails);
    }
}