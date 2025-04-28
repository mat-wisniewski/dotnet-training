using Moq;
using NUnit.Framework;
using training.DiceRollGame.App;
using training.DiceRollGame.Enums;
using training.DiceRollGame.UserInteraction;

namespace training.DiceRollGame.Tests
{
  [TestFixture]
  public class DiceRollGameUnitTests
  {
    private Mock<IDice> _diceMock;
    private Mock<IUserInteractor> _userInteractorMock;
    private DiceGame _cut;

    [SetUp]
    public void Setup()
    {
      _diceMock = new Mock<IDice>();
      _userInteractorMock = new Mock<IUserInteractor>();
      _cut = new(_diceMock.Object, _userInteractorMock.Object);
    }

    [Test]
    public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnTheFirstTry()
    {
      const int NumberOnDie = 1;
      _diceMock
        .Setup(m => m.Roll())
        .Returns(NumberOnDie);
      _userInteractorMock
        .Setup(m => m.ReadInteger(It.IsAny<string>()))
        .Returns(NumberOnDie);

      var gameResult = _cut.Play();

      Assert.That(gameResult, Is.EqualTo(GameResult.Vicotory));
    }

    [Test]
    public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnTheThirdTry()
    {
      SetupUserGuessingTheNumberOnThirdTry();

      var gameResult = _cut.Play();

      Assert.That(gameResult, Is.EqualTo(GameResult.Vicotory));
    }

    [Test]
    public void Play_ShallReturnLoss_IfTheUserGuessesTheNumberOnTheFourthTry()
    {
      const int NumberOnDie = 1;
      _diceMock
        .Setup(m => m.Roll())
        .Returns(NumberOnDie);
      _userInteractorMock
        .SetupSequence(m => m.ReadInteger(It.IsAny<string>()))
        .Returns(6)
        .Returns(5)
        .Returns(4)
        .Returns(NumberOnDie);

      var gameResult = _cut.Play();

      Assert.That(gameResult, Is.EqualTo(GameResult.Loss));
    }

    [Test]
    public void Play_ShallReturnLoss_IfTheUserNeverGuessesTheNumber()
    {
      const int NumberOnDie = 1;
      _diceMock
        .Setup(m => m.Roll())
        .Returns(NumberOnDie);
      const int UserNumber = 2;
      _userInteractorMock
        .Setup(m => m.ReadInteger(It.IsAny<string>()))
        .Returns(UserNumber);

      var gameResult = _cut.Play();

      Assert.That(gameResult, Is.EqualTo(GameResult.Loss));
    }

    [TestCase(GameResult.Vicotory)]
    public void DisplayResult_ShallShowVictoryMessage_IfGameIsWon(GameResult gameResult)
    {
      _cut.DisplayResult(gameResult);

      _userInteractorMock
        .Verify(m => m.ShowMessage(Resource.VictoryMessage));
    }

    [TestCase(GameResult.Loss)]
    public void DisplayResult_ShallShowLossMessage_IfGameIsLoss(GameResult gameResult)
    {
      _cut.DisplayResult(gameResult);

      _userInteractorMock
        .Verify(m => m.ShowMessage(Resource.LossMessage));
    }

    [Test]
    public void Play_ShallShowWelcomeMessage()
    {
      var gameResult = _cut.Play();

      _userInteractorMock.Verify(m => m.ShowMessage(
        string.Format(Resource.WelcomeMessage, Resource.InitialTries)),
        Times.Once);
    }

    [Test]
    public void Play_ShallAskForNumberThreeTimes_IfTheUserGuessesTheNumberOnTheThirdTry()
    {
      SetupUserGuessingTheNumberOnThirdTry();

      var gameResult = _cut.Play();

      _userInteractorMock.Verify(m => m.ReadInteger(
        Resource.EnterNumberMessage),
        Times.Exactly(3));
    }

    [Test]
    public void Play_ShallShowWrongNumberMessageTwice_IfTheUserGuessesTheNumberOnTheThirdTry()
    {
      SetupUserGuessingTheNumberOnThirdTry();

      var gameResult = _cut.Play();

      _userInteractorMock.Verify(m => m.ShowMessage(
        Resource.WrongNumberMessage),
        Times.Exactly(2));
    }

    private void SetupUserGuessingTheNumberOnThirdTry()
    {
      const int NumberOnDie = 1;
      _diceMock
        .Setup(m => m.Roll())
        .Returns(NumberOnDie);
      _userInteractorMock
        .SetupSequence(m => m.ReadInteger(It.IsAny<string>()))
        .Returns(6)
        .Returns(5)
        .Returns(NumberOnDie);
    }
  }
}
