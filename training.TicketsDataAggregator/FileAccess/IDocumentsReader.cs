﻿namespace training.TicketsDataAggregator.FileAccess
{
  public interface IDocumentsReader
  {
    IEnumerable<string> Read(string directory);
  }
}