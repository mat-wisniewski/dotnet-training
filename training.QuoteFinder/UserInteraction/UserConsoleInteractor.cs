
namespace training.QuoteFinder.UserInteraction
{
  public class UserConsoleInteractor : IUserInteractor
  {
    public bool ReadBoolean(string message)
    {
      Console.WriteLine(message + " ('y/Y' for 'yes', anything elese for 'no')");

      var result = Console.ReadLine();

      if (string.Equals(result, "y", StringComparison.OrdinalIgnoreCase))
      {
        return true;
      }
      else
      {
        return false;
      }

    }

    public int ReadInteger(string message)
    {
      int result;
      do
      {
        Console.WriteLine(message);
        var stringResult = Console.ReadLine();
        var isValid = int.TryParse(stringResult, out result);
      } while (!IsValidInteger(result));
      return result;
    }

    private bool IsValidInteger(int input)
    {
      return input > 0;
    }

    public string ReadSingleWord(string message)
    {
      string result;
      do
      {
        Console.WriteLine(message);
        result = Console.ReadLine();
      } while (!IsValidWord(result));
      return result;
    }

    public void ShowMessage(string message)
    {
      Console.WriteLine(message);
    }

    private bool IsValidWord(string input)
    {
      return input is not null && input.All(char.IsLetter);
    }
  }
}
