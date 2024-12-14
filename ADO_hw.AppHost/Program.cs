var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ADO_hw>("ado-hw");

builder.Build().Run();
