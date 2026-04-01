using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Cosmos;
using radumitreacomfunctions;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services
    .AddSingleton(new CosmosClient(
        Environment.GetEnvironmentVariable("CosmosDbConnectionString"),
        new CosmosClientOptions
        {
            Serializer = new NewtonsoftCosmosSerializer()
        }
    ));

builder.Build().Run();
