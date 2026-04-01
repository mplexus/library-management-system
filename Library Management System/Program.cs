using Library_Management_System.Models;
using Library_Management_System.Services;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    //.ConfigureFunctionsApplicationInsights()
    .AddTransient<ILibrary, Library>()
    ;

builder.Build().Run();
