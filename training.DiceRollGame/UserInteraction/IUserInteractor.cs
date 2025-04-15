namespace training.DiceRollGame.UserInteraction
{
  public interface IUserInteractor
  {
    int ReadInteger(string message);
    void ShowMessage(string message);
  }
}
