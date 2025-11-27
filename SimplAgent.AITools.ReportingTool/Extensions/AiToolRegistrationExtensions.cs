using Microsoft.Extensions.DependencyInjection;
using SimplAgent.AITools.ReportingTool.Services;

namespace SimplAgent.AITools.ReportingTool.Extensions;

public static class AiToolRegistrationExtensions
{
    public static IMcpServerBuilder AddAiTool(this IMcpServerBuilder mcpServerBuilder)
    {
        mcpServerBuilder
            .WithToolsFromAssembly(typeof(AiToolRegistrationExtensions).Assembly);

        var services = mcpServerBuilder.Services;
        services.AddScoped<IProductSaleService, ProductSaleService>();

        return mcpServerBuilder;
    }
}