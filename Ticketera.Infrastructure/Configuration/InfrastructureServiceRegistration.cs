using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticketera.Domain.Interfaces;
using Ticketera.Infrastructure.Models;
using Ticketera.Infrastructure.Repositories;

namespace Ticketera.Infrastructure.Configuration;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TicketeraBdContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<Ticketera.Application.Interfaces.IExcelService, Ticketera.Infrastructure.Services.ExcelService>();
        return services;
    }
}