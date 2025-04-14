using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace IdentityService.Application;

public static class ServiceRegistration
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));
    }
}
