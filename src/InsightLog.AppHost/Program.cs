var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.InsightLog_API>("insightlog-api");

builder.Build().Run();
