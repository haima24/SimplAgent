var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.SimpleAgent_Web_ApiService>("apiservice");

builder.AddProject<Projects.SimpleAgent_Web_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
