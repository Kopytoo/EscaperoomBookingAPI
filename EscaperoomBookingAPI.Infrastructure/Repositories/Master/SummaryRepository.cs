using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Common;
using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using EscaperoomBookingAPI.Infrastructure.Repositories.Common;
using Microsoft.Extensions.Logging;

namespace EscaperoomBookingAPI.Infrastructure.Repositories.Master;

public class SummaryRepository : GenericRepository<Summary, Guid>, ISummaryRepository
{
    public SummaryRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {
    }
}