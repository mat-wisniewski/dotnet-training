namespace training.QuoteFinder.UserInteraction
{
  public interface IUserInteractor
  {
    string ReadSingleWord(string message);
    int ReadInteger(string message);
    bool ReadBoolean(string message);
    void ShowMessage(string message);
  }
}
