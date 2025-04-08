namespace training.QuoteFinder
{
  public static class StringExtensions
  {
    public static bool ContainsWord(this string input, string requiredWord)
    {
      var split = input.Split(
        [' ', '.', ',', '!', '?', ';', ':'],
        StringSplitOptions.RemoveEmptyEntries);

      return split.Any(word => string.Equals(
        word, requiredWord, StringComparison.OrdinalIgnoreCase));
    }
  }
}
