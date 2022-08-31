using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
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
    
    // GET
    [HttpGet]
    // [ProducesResponseType()]
    public async Task<IActionResult> GetAll()
    {
        var summaryDtos = await _unitOfWork.Summaries.GetAllSummariesAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(summaryDtos);
    }
    
    // GET
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var summaryDto = await _unitOfWork.Summaries.GetSummaryByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(summaryDto);
    }

    // POST
    [HttpPost]
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
}