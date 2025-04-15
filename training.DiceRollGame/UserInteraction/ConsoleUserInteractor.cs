namespace training.DiceRollGame.UserInteraction
{
  public class ConsoleUserInteractor : IUserInteractor
  {
    public int ReadInteger(string message)
    {
      int result;
      do
      {
        Console.WriteLine(message);
      } while (!int.TryParse(Console.ReadLine(), out result));
      return result;
    }

    public void ShowMessage(string message)
    {
      Console.WriteLine(message);
    }
  }
}
