namespace training.QuoteFinder.Models
{
  public class Datum
  {
    public string _id { get; set; } = string.Empty;
    public string quoteText { get; set; } = string.Empty;
    public string quoteAuthor { get; set; } = string.Empty;
    public string quoteGenre { get; set; } = string.Empty;
    public int __v { get; set; }
  }
}
