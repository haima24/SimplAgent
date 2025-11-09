using SimplAgent.Web.ApiService.Endpoints; 
using SimplAgent.Core.Contracts;
using SimplAgent.Core.Implementations;
using SimplAgent.Shared.Dtos.Configuration;
using SimplAgent.Web.ApiService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add required services
builder.AddServiceDefaults();
builder.Services.ReadConfigurations(builder.Configuration);
builder.Services.AddScoped<IAgentFactory, AgentFactory>();
builder.Services.Configure<AgentAIConfig>(builder.Configuration.GetSection("AgentAIConfig"));
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapAgentsApi();

app.MapDefaultEndpoints();

app.Run();
