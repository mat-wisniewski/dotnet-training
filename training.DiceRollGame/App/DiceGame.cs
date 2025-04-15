using training.DiceRollGame.Enums;
using training.DiceRollGame.UserInteraction;

namespace training.DiceRollGame.App
{
  public class DiceGame(Dice dice, IUserInteractor userInteractor)
  {
    private readonly Dice _dice = dice;
    private readonly IUserInteractor _userInteractor = userInteractor;
    private const int InitialTries = 3;

    public GameResult Play()
    {
      var diceRollResult = _dice.Roll();
      _userInteractor.ShowMessage($"Dice rolled. Guess the number in {InitialTries} tries");

      var triesLeft = InitialTries;

      while (triesLeft > 0)
      {
        var guess = _userInteractor.ReadInteger($"Enter a number: ({triesLeft} tries left)");

        if (guess == diceRollResult)
        {
          return GameResult.Vicotory;
        }

        --triesLeft;
      }

      return GameResult.Loss;
    }

    public void DisplayResult(GameResult result)
    {
      string message = result == GameResult.Vicotory ?
        "You win!" :
        "You lose.";

      _userInteractor.ShowMessage(message);
    }
  }
}
