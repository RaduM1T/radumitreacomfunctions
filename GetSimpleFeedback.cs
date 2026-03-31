using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace radumitreacomfunctions;

public class GetSimpleFeedback
{
    private readonly ILogger<GetSimpleFeedback> _logger;

    public GetSimpleFeedback(ILogger<GetSimpleFeedback> logger)
    {
        _logger = logger;
    }

    [Function("GetSimpleFeedback")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}