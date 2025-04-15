using System.Globalization;
using System.Text;
using training.TicketsDataAggregator.Extensions;
using training.TicketsDataAggregator.FileAccess;

namespace training.TicketsDataAggregator.TicketsAggregation
{
  public class TicketsAggregator
  {
    private readonly IFileWriter _fileWriter;
    private readonly IDocumentsReader _documentsReader;
    private string _ticketsFolder;
    private readonly Dictionary<string, CultureInfo> _domaintToCultureMapping = new()
    {
      [".com"] = new CultureInfo("en-Us"),
      [".fr"] = new CultureInfo("fr-FR"),
      [".jp"] = new CultureInfo("ja-JP")
    };

    public TicketsAggregator(
      IFileWriter fileWriter,
      IDocumentsReader documentsReader,
      string ticketsFolder)
    {
      _fileWriter = fileWriter;
      _documentsReader = documentsReader;
      _ticketsFolder = ticketsFolder;
    }

    public void Run()
    {
      var stringBuilder = new StringBuilder();
      foreach (var document in _documentsReader.Read(_ticketsFolder))
      {
        var lines = ProcessDocument(document);

        stringBuilder.AppendLine(string.Join(Environment.NewLine, lines));
      }

      _fileWriter.Write(
        stringBuilder.ToString(),
        [_ticketsFolder, "aggregatedTickets.txt"]);
    }

    private IEnumerable<string> ProcessDocument(string document)
    {
      var split = document.Split(
        ["Title:", "Date:", "Time:", "Visit us:"],
        StringSplitOptions.None);

      var domain = split.Last().ExtractDomain();
      var ticketCulture = _domaintToCultureMapping[domain];

      for (int i = 1; i < split.Length - 3; i += 3)
      {
        yield return BuildTicketData(split, i, ticketCulture);
      }
    }

    private static string BuildTicketData(string[] split, int i, CultureInfo ticketCulture)
    {
      var title = split[i];
      var timeAsString = split[i + 2];
      var dateAsString = split[i + 1];

      var time = TimeOnly.Parse(
        timeAsString, ticketCulture);

      var date = DateOnly.Parse(
        dateAsString, ticketCulture);

      var timeAsStringInvariant = time.ToString(CultureInfo.InvariantCulture);
      var dateAsStringInvariant = date.ToString(CultureInfo.InvariantCulture);

      var ticketData = $"{title,-40}|{dateAsStringInvariant}|{timeAsStringInvariant}";

      return ticketData;
    }
  }
}