var builder = DistributedApplication.CreateBuilder(args);

var mcpServer = builder.AddProject<Projects.SimplAgent_MCP_Server>("mcpserver");
var knowledgeBaseService = builder.AddProject<Projects.SimplAgent_Knowledge>("knowledgebaseservice");

var apiService = builder.AddProject<Projects.SimplAgent_Web_ApiService>("apiservice")
    .WithExternalHttpEndpoints()
    .WithReference(mcpServer)
    .WithReference(knowledgeBaseService);

builder.AddProject<Projects.SimplAgent_Web_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
