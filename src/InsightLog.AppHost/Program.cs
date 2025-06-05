var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.InsightLog_API>("insightlog-api");
builder.AddProject<Projects.InsightLog_Blazor>("insightlog-blazor")
       .WithReference(api);

builder.Build().Run();
