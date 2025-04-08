using training.QuoteFinder.App;
using training.QuoteFinder.DataAccess;
using training.QuoteFinder.DataAccess.Mock;
using training.QuoteFinder.UserInteraction;

namespace training.QuoteFinder
{
  public class Program
  {
    static async Task Main(string[] args)
    {
      IUserInteractor userInteractor = new UserConsoleInteractor();
      IQuotesApiDataReader quotesApiDataReader = new MockQuotesApiDataReader();

      IQuoteDataFetcher quoteDataFetcher = new QuoteDataFetcher(quotesApiDataReader);
      IQuoteDataProcessor quoteDataProcessor = new QuoteDataProcessor(userInteractor);

      try
      {
        string word = userInteractor.ReadSingleWord(
          "What word are you looking for?");

        int numberOfPages = userInteractor.ReadInteger(
          "How many pages you want to read?");

        int quotesPerPage = userInteractor.ReadInteger(
          "How many quotes per page?");

        bool shallProcessInParallel = userInteractor.ReadBoolean(
          "Shall process pages in parallel?");

        userInteractor.ShowMessage("Fetching data...");

        IEnumerable<string> data = await quoteDataFetcher.FetchDataFromAllPagesAsync(
          numberOfPages,
          quotesPerPage);
        userInteractor.ShowMessage("Data is ready.");

        await quoteDataProcessor.ProcessAsync(data, word, shallProcessInParallel);

      }
      catch (Exception ex)
      {
        userInteractor.ShowMessage("Exception thorwn: " + ex.Message);
      }

      Console.WriteLine("Finished.");
      Console.ReadKey();
    }
  }
}
