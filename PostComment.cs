using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace radumitreacomfunctions;

public class PostComment
{
    private readonly ILogger<PostComment> _logger;
    private readonly CosmosClient _cosmosClient;

    public PostComment(ILogger<PostComment> logger, CosmosClient cosmosClient)
    {
        _logger = logger;
        _cosmosClient = cosmosClient;
    }

    [Function("PostComment")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var comment = JsonConvert.DeserializeObject<Comment>(body);

        if (comment == null || string.IsNullOrEmpty(comment.Slug))
            return new BadRequestObjectResult("Invalid comment payload.");

        comment.Id = Guid.NewGuid().ToString();
        comment.CreatedAt = DateTime.UtcNow;

        var container = _cosmosClient.GetContainer("radumitreadb", "comments");
        await container.CreateItemAsync(comment, new PartitionKey(comment.Slug));

        _logger.LogInformation("Comment created for slug: {Slug}", comment.Slug);
        return new OkObjectResult(comment);
    }
}