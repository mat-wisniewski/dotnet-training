using training.DiceRollGame.App;
using training.DiceRollGame.Enums;
using training.DiceRollGame.UserInteraction;

namespace training.DiceRollGame
{
  public partial class Program
  {
    static void Main(string[] args)
    {
      var random = new Random();
      IDice dice = new Dice(random);
      IUserInteractor userInteractor = new ConsoleUserInteractor();

      var diceGame = new DiceGame(dice, userInteractor);

      GameResult gameResult = diceGame.Play();
      diceGame.DisplayResult(gameResult);

      Console.WriteLine("Press any key to close.");
      Console.ReadKey();
    }
  }
}
