using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace EscaperoomBookingAPI.Presentation.Web.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class SummaryController : Controller
{
    private readonly ILogger<SummaryController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SummaryController(ILogger<SummaryController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var summaryDtos = await _unitOfWork.Summaries.GetAllSummariesAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(summaryDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var summaryDto = await _unitOfWork.Summaries.GetSummaryByIdAsync(id);

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
        if (ModelState.IsValid)
        {
            var newSummary = await _unitOfWork.Summaries.CreateSummaryAsync();
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetById", new { newSummary.Id }, newSummary);
        }

        return new JsonResult("Something Went Wrong") { StatusCode = 500 };
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(SummaryDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid summaryId, [FromForm] BookingStatus status)
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.Summaries.UpdateSummaryStatusAsync(summaryId, status);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        return new JsonResult("Something Went Wrong") { StatusCode = 500 };
    }
}