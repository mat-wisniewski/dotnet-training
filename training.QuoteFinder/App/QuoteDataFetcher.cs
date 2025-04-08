
using training.QuoteFinder.DataAccess;

namespace training.QuoteFinder.App
{
  public class QuoteDataFetcher(IQuotesApiDataReader quotesApiDataReader) : IQuoteDataFetcher
  {
    private readonly IQuotesApiDataReader _quotesApiDataReader = quotesApiDataReader;

    public async Task<IEnumerable<string>> FetchDataFromAllPagesAsync(
      int numberOfPages,
      int quotesPerPage)
    {
      var tasks = new List<Task<string>>();

      for (int i = 0; i < numberOfPages; ++i)
      {
        var fetchDataTask = _quotesApiDataReader.ReadAsync(i + 1, quotesPerPage);
        tasks.Add(fetchDataTask);
      }

      return await Task.WhenAll(tasks);
    }
  }
}
