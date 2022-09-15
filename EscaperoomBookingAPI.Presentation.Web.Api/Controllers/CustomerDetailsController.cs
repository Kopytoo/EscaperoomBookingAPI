using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscaperoomBookingAPI.Presentation.Web.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerDetailsController : Controller
{
    private readonly ILogger<SummaryController> _logger;
    private readonly ISummaryService _summaryService;
    private readonly ICustomerDetailsService _customerDetailsService;

    public CustomerDetailsController(ILogger<SummaryController> logger, ISummaryService summaryService, ICustomerDetailsService customerDetailsService)
    {
        _logger = logger;
        _summaryService = summaryService;
        _customerDetailsService = customerDetailsService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingDetailsDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var customerDetailsDtos = await _customerDetailsService.GetAllCustomerDetailsAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(customerDetailsDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var customerDetailsDto = await _customerDetailsService.GetCustomerDetailsByIdAsync(id);

        if (customerDetailsDto == null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(customerDetailsDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBySummaryId([FromRoute] Guid id)
    {
        var customerDetailsDto = await _customerDetailsService.GetCustomerDetailsBySummaryIdAsync(id);

        if (customerDetailsDto == null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(customerDetailsDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] Guid summaryId, [FromForm] CustomerDetailsDto customerDetails)
    {
        if (_summaryService.GetSummaryByIdAsync(summaryId) == null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var newCustomerDetails = await _customerDetailsService.CreateCustomerDetailsAsync(summaryId, customerDetails);
        await _summaryService.UpdateSummaryAsync(summaryId, Guid.Empty, newCustomerDetails.Id);

        return CreatedAtAction("GetById", new { newCustomerDetails.Id }, newCustomerDetails);
    }
}