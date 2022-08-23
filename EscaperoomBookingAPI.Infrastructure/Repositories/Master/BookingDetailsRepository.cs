using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using EscaperoomBookingAPI.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EscaperoomBookingAPI.Infrastructure.Repositories.Master;

public class BookingDetailsRepository : GenericRepository<BookingDetails, Guid>, IBookingDetailsRepository
{
    public BookingDetailsRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}