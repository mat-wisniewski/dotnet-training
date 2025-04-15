using training.TicketsDataAggregator.FileAccess;
using training.TicketsDataAggregator.TicketsAggregation;

namespace training.TicketsDataAggregator
{
  internal class Program
  {
    static void Main(string[] args)
    {
      const string TicketsFolder = @"C:\mat-wisniewski\dotnet-training\dotnet-basic\training\Tickets";

      try
      {
        var fileWriter = new FileWriter();
        var documentsReader = new DocumentsFromPdfsReader();
        var ticketsAggregator = new TicketsAggregator(
          fileWriter,
          documentsReader,
          TicketsFolder);

        ticketsAggregator.Run();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception message: " + ex.Message);
      }

      Console.WriteLine("Press any key to close.");
      Console.ReadLine();
    }
  }
}
