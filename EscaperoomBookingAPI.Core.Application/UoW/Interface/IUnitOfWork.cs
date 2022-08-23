using EscaperoomBookingAPI.Core.Application.Repositories.Interfaces.Master;

namespace EscaperoomBookingAPI.Core.Application.UoW.Interface;

public interface IUnitOfWork
{
    IBookingDetailsRepository BookingsDetails { get; }
    ICustomerDetailsRepository CustomersDetails { get; }
    ISummaryRepository Summaries { get; }

    Task SaveChangesAsync();
}