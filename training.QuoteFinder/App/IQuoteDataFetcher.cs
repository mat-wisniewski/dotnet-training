namespace training.QuoteFinder.App
{
  public interface IQuoteDataFetcher
  {
    Task<IEnumerable<string>> FetchDataFromAllPagesAsync(
      int numberOfPages,
      int quotesPerPage);
  }
}
