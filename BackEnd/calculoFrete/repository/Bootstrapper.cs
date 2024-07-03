using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace services
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDomainRepositories(
            this IServiceCollection services)
        {
            return services.AddMediatR(typeof(Bootstrapper).Assembly);
        }
    }
}