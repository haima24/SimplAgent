using SimplAgent.AITools.ReportingTool.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly()
    .AddAiTool();

var app = builder.Build();

app.MapMcp();

app.MapDefaultEndpoints();

app.Run();
