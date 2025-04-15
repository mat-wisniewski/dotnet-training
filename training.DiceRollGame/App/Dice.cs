namespace training.DiceRollGame.App
{
  public class Dice(Random random)
  {
    private readonly Random _random = random;
    private const int DMax = 6;

    public int Roll() => _random.Next(1, DMax + 1);
  }
}
