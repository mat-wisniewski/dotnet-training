﻿
using System.Text.Json;
using training.QuoteFinder.Models;
using training.QuoteFinder.UserInteraction;

namespace training.QuoteFinder.App
{
  public class QuoteDataProcessor(IUserInteractor userInteractor) : IQuoteDataProcessor
  {
    private readonly IUserInteractor _userInteractor = userInteractor;
    public async Task ProcessAsync(
      IEnumerable<string> data,
      string word,
      bool shallProcessInParallel)
    {
      if (shallProcessInParallel)
      {
        _userInteractor.ShowMessage(
          "Parallel processing started" + Environment.NewLine);

        var tasks = data.Select(page => Task.Run(() => ProcessPage(page, word)));

        await Task.WhenAll(tasks);
      }
      else
      {
        _userInteractor.ShowMessage(
          "Sequential processing started" + Environment.NewLine);
        foreach (var page in data)
        {
          ProcessPage(page, word);
        }
      }
    }

    private void ProcessPage(string page, string word)
    {
      var root = JsonSerializer.Deserialize<Root>(page);
      var quoteWithWord = root?.data
        .Where(quote => quote.quoteText.ContainsWord(word))
        .MinBy(quote => quote.quoteText.Length);

      if (quoteWithWord is not null)
      {
        _userInteractor.ShowMessage(
          $"{quoteWithWord.quoteText} -- {quoteWithWord.quoteAuthor}");
      }
      else
      {
        _userInteractor.ShowMessage("No quote found on this page");
      }

      _userInteractor.ShowMessage(string.Empty);
    }
  }
}
