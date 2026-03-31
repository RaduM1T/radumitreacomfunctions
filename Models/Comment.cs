public class Comment
{
  public required string Id { get; set; }                                                  
  public required string Slug { get; set; }                                                
  public string? Author { get; set; }
  public string? Email { get; set; }                                                       
  public string? Message { get; set; }
  public bool? Approved { get; set; }                                              
  public DateTime? CreatedAt { get; set; }
}
