var builder = DistributedApplication.CreateBuilder(args);

var knowledgeBaseService = builder.AddProject<Projects.SimplAgent_Knowledge>("knowledgebaseservice");

var apiService = builder.AddProject<Projects.SimplAgent_Web_ApiService>("apiservice")
    .WithExternalHttpEndpoints()
    .WithReference(knowledgeBaseService);

builder.AddProject<Projects.SimplAgent_Web_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
