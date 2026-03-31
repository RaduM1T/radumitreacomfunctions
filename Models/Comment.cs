using Newtonsoft.Json;

public class Comment
{
  [JsonProperty("id")]
  public required string Id { get; set; }
  [JsonProperty("slug")]
  public required string Slug { get; set; }
  [JsonProperty("author")]
  public string? Author { get; set; }
  [JsonProperty("email")]
  public string? Email { get; set; }
  [JsonProperty("message")]
  public string? Message { get; set; }
  [JsonProperty("approved")]
  public bool? Approved { get; set; }
  [JsonProperty("createdon")]
  public DateTime? CreatedAt { get; set; }
}
