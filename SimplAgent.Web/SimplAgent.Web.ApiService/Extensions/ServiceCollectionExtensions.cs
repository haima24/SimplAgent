using SimplAgent.Core.Configuration;

namespace SimplAgent.Web.ApiService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ReadConfigurations(
           this IServiceCollection services,
           IConfiguration configuration
        )
        {
            services.Configure<AgentAIConfig>(configuration.GetSection("AgentConfig"));

            return services;
        }
    }
}
