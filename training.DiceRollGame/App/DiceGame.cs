using training.DiceRollGame.Enums;
using training.DiceRollGame.UserInteraction;

namespace training.DiceRollGame.App
{
  public class DiceGame(IDice dice, IUserInteractor userInteractor)
  {
    private readonly IDice _dice = dice;
    private readonly IUserInteractor _userInteractor = userInteractor;
    private readonly int InitialTries = int.Parse(Resource.InitialTries);

    public GameResult Play()
    {
      var diceRollResult = _dice.Roll();
      _userInteractor.ShowMessage(
        string.Format(Resource.WelcomeMessage, Resource.InitialTries));

      var triesLeft = InitialTries;

      while (triesLeft > 0)
      {
        var guess = _userInteractor.ReadInteger(Resource.EnterNumberMessage);

        if (guess == diceRollResult)
        {
          return GameResult.Vicotory;
        }
        _userInteractor.ShowMessage(Resource.WrongNumberMessage);
        --triesLeft;
      }

      return GameResult.Loss;
    }

    public void DisplayResult(GameResult result)
    {
      string message = result == GameResult.Vicotory ?
        Resource.VictoryMessage :
        Resource.LossMessage;

      _userInteractor.ShowMessage(message);
    }
  }
}
