var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.DevAcademy_Api>("api");

builder.AddProject<Projects.DevAcademy_Gateway>("gateway")
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();