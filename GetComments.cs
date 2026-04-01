
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace radumitreacomfunctions;

public class GetComments
{
    private readonly ILogger<GetComments> _logger;
    private readonly CosmosClient _cosmosClient;

    public GetComments(ILogger<GetComments> logger,CosmosClient cosmosClient)
    {
        _logger = logger;
        _cosmosClient = cosmosClient;
    }

    [Function("GetComments")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        var slug = req.Query["slug"];
        string slugValue = slug.ToString();
        var commentsContainer = _cosmosClient.GetContainer("radumitreadb","comments");
        
        QueryDefinition query = new QueryDefinition("SELECT * FROM c WHERE c.slug = @slug and c.approved = true").WithParameter("@slug", slugValue);

        var queryOptions = new QueryRequestOptions
        {
            PartitionKey = new PartitionKey(slugValue.ToString())
        };

        var iterator = commentsContainer.GetItemQueryIterator<Comment>(query, requestOptions: queryOptions);

        var comments = new List<Comment>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            comments.AddRange(response);
        }

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult(comments);
    }
}