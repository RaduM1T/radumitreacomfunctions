public class Comment
{
  public required string id { get; set; }                                                  
  public required string slug { get; set; }                                                
  public string? author { get; set; }
  public string? email { get; set; }                                                       
  public string? message { get; set; }
  public bool? approved { get; set; }                                              
  public DateTime createdon { get; set; }
}
