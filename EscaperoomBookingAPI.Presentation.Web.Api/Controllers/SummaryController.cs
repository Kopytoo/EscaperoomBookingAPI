using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EscaperoomBookingAPI.Presentation.Web.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SummaryController : Controller
{
    private readonly ILogger<SummaryController> _logger;
    private readonly ISummaryService _summaryService;

    public SummaryController(ILogger<SummaryController> logger, ISummaryService summaryService)
    {
        _logger = logger;
        _summaryService = summaryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var summaryDtos = await _summaryService.GetAllSummariesAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(summaryDtos);
    }
    
    [HttpGet("{room}")]
    [ProducesResponseType(typeof(IEnumerable<SummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByRoom([FromRoute] Room room)
    {
        var summaryDtos = await _summaryService.GetSummariesByRoomAsync(room);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(summaryDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var summaryDto = await _summaryService.GetSummaryByIdAsync(id);

        if (summaryDto == null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(summaryDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newSummary = await _summaryService.CreateSummaryAsync();

        return CreatedAtAction("GetById", new { newSummary.Id }, newSummary);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStatus([FromForm] Guid summaryId, [FromForm] BookingStatus status)
    {
        if (_summaryService.GetSummaryByIdAsync(summaryId) == null)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _summaryService.UpdateSummaryStatusAsync(summaryId, status);

        return NoContent();
    }
}