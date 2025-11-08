using SimplAgent.Shared.Dtos.Configuration;

namespace SimplAgent.Web.ApiService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ReadConfigurations(
           this IServiceCollection services,
           IConfiguration configuration
        )
        {
            services.Configure<AzureOpenAIConfig>(configuration.GetSection("AgentConfig"));

            return services;
        }
    }
}
