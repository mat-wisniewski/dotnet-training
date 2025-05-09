﻿namespace training.QuoteFinder.App
{
  public interface IQuoteDataProcessor
  {
    Task ProcessAsync(
      IEnumerable<string> data,
      string word,
      bool shallProcessInParallel);
  }
}
