using EscaperoomBookingAPI.Core.Application.Context.Interface;
using EscaperoomBookingAPI.Core.Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;

namespace EscaperoomBookingAPI.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<BookingDetails> BookingDetails { get; set; }
    public virtual DbSet<CustomerDetails> CustomerDetails { get; set; }
    public virtual DbSet<Summary> Summary { get; set; }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Summary>()
            .HasOne(s => s.BookingDetails)
            .WithOne(b => b.Summary)
            .HasForeignKey<BookingDetails>(b => b.SummaryReference)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Summary>()
            .HasOne(s => s.CustomerDetails)
            .WithOne(c => c.Summary)
            .HasForeignKey<CustomerDetails>(c => c.SummaryReference)
            .OnDelete(DeleteBehavior.Cascade);
    }
}