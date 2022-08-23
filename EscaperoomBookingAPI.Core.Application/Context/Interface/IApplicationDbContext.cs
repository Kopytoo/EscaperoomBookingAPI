using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;

namespace EscaperoomBookingAPI.Core.Application.Context.Interface;

public interface IApplicationDbContext
{
    DbSet<BookingDetails> BookingDetails { get; set; }
    DbSet<CustomerDetails> CustomerDetails { get; set; }
    DbSet<Summary> Summary { get; set; }

    public Task<int> SaveChangesAsync();
}