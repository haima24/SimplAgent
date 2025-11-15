using Microsoft.KernelMemory;
using SimplAgent.Core.Implementations;
using SimplAgent.Web.ApiService.Configuration;
using SimplAgent.Web.ApiService.Contracts;
using SimplAgent.Web.ApiService.Endpoints;
using SimplAgent.Web.ApiService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add required services
builder.AddServiceDefaults();
builder.Services.ReadConfigurations(builder.Configuration);
builder.Services.AddScoped<IAgentFactory, AgentFactory>();
builder.Services.Configure<AgentAIConfig>(builder.Configuration.GetSection("AgentAIConfig"));
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IKernelMemory>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var endpoint = Environment.GetEnvironmentVariable($"services__simplagent-knowledge__https__0")
                   ?? Environment.GetEnvironmentVariable($"services__simplagent-knowledge__http__0")
                   ?? throw new InvalidOperationException("KernelMemory Endpoint configuration is required");
    var apiKey = configuration["KernelMemory:ApiKey"]
                ?? throw new InvalidOperationException("KernelMemory:ApiKey configuration is required");

    return new MemoryWebClient(endpoint, apiKey);
});

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapAgentsApi();

app.MapDefaultEndpoints();

app.Run();
