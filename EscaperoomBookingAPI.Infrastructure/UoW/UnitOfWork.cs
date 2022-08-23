using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;
using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using EscaperoomBookingAPI.Infrastructure.Repositories.Master;
using Microsoft.Extensions.Logging;

namespace EscaperoomBookingAPI.Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;

    public IBookingDetailsRepository BookingsDetails { get; }
    public ICustomerDetailsRepository CustomersDetails { get; }
    public ISummaryRepository Summaries { get; }

    public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = loggerFactory.CreateLogger("logs");

        BookingsDetails = new BookingDetailsRepository(context, _logger);
        CustomersDetails = new CustomerDetailsRepository(context, _logger);
        Summaries = new SummaryRepository(context, _logger);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}