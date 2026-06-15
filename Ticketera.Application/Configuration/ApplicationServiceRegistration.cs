using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Ticketera.Application.Configuration;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Configuración de MediatR utilizando el Assembly actual
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}