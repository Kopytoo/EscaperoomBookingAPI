using EscaperoomBookingAPI.Core.Application.Context.Interface;
using EscaperoomBookingAPI.Core.Application.Services;
using EscaperoomBookingAPI.Core.Application.UoW.Interface;
using EscaperoomBookingAPI.Core.Domain.Services.Interfaces;
using EscaperoomBookingAPI.Infrastructure.Persistence.Context;
using EscaperoomBookingAPI.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EscaperoomBookingAPI.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("Db"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Transient);

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<ISummaryService, SummaryService>();
        //services.AddScoped<ICustomerDetailsService, CustomerDetailsService>();
        services.AddScoped<IBookingDetailsService, BookingDetailsService>();

        return services;
    }
}