using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EscaperoomBookingAPI.Presentation.Web.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerDetailsController : Controller
{
    private readonly ILogger<BookingDetailsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerDetailsController(ILogger<BookingDetailsController> logger, IUnitOfWork unitOfWork)
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
        var customerDetailsDtos = await _unitOfWork.CustomersDetails.GetAllCustomerDetailsAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(customerDetailsDtos);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customerDetailsDto = await _unitOfWork.CustomersDetails.GetCustomerDetailsByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(customerDetailsDto);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBySummaryId(Guid id)
    {
        var customerDetailsDto = await _unitOfWork.CustomersDetails.GetCustomerDetailsBySummaryIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(customerDetailsDto);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(BookingDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(Guid summaryId, string name, string email, int phoneNumber, string? otherInfo)
    {
        if (ModelState.IsValid)
        {
            var newCustomerDetails = await _unitOfWork.CustomersDetails.CreateCustomerDetailsAsync(summaryId, name, email, phoneNumber, otherInfo);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.Summaries.UpdateSummaryAsync(summaryId, Guid.Empty, newCustomerDetails.Id);
            await _unitOfWork.SaveChangesAsync();  
            
            return CreatedAtAction("GetById", new { newCustomerDetails.Id }, newCustomerDetails);
        }

        return new JsonResult("Something Went Wrong") { StatusCode = 500 };
    }
}