using Microsoft.EntityFrameworkCore;
using Microsoft.KernelMemory;
using NTG.Agent.Orchestrator.Services.Knowledge;
using SimplAgent.Web.ApiService.Configuration;
using SimplAgent.Web.ApiService.Data;
using SimplAgent.Web.ApiService.Extensions;
using SimplAgent.Web.ApiService.Routes;
using SimplAgent.Web.ApiService.Services.AgentFactory;

var builder = WebApplication.CreateBuilder(args);

// Add required services
builder.AddServiceDefaults();
builder.Services.ReadConfigurations(builder.Configuration);
builder.Services.AddScoped<IAgentFactory, AgentFactory>();
builder.Services.AddScoped<IKnowledgeService, KernelMemoryKnowledge>();
builder.Services.Configure<AgentAIConfig>(builder.Configuration.GetSection("AgentAIConfig"));
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IKernelMemory>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var endpoint = Environment.GetEnvironmentVariable($"services__knowledgebaseservice__https__0")
                   ?? Environment.GetEnvironmentVariable($"services__knowledgebaseservice__http__0")
                   ?? throw new InvalidOperationException("KernelMemory Endpoint configuration is required");
    var apiKey = configuration["KernelMemory:ApiKey"]
                ?? throw new InvalidOperationException("KernelMemory:ApiKey configuration is required");

    return new MemoryWebClient(endpoint, apiKey);
});

builder.AddServiceDefaults();

builder.Services.AddDbContext<AgentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "SimplAgent API");
        options.DocumentTitle = "SimplAgent API Explorer";
    });
}

app.MapAgentsApi();
app.MapKnowledgeApi();

app.MapDefaultEndpoints();

app.Run();
