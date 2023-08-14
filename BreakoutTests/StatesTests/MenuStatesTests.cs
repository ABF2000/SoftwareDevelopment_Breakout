using NUnit.Framework;
using Breakout;
using DIKUArcade.State;
using Breakout.BreakoutStates;


namespace BreakoutTests;

public class MenuStatesTests{



    [SetUp]
    public void SetupTest(){
        GameLost.GetInstance().ResetState();
        GameWon.GetInstance().ResetState();
        GamePaused.GetInstance().ResetState();
        MainMenu.GetInstance().ResetState();

    }

    [Test]
    public void TestNumberOfButtonsMain(){
        Assert.AreEqual(2, MainMenu.GetInstance().GetMenu().Length);
    }

    [Test]
    public void TestInitialActiveButtonMain(){
        Assert.AreEqual(0, MainMenu.GetInstance().GetActiveMenuButton());
    }


    [Test]
    public void TestNumberOfButtonsLost(){
        Assert.AreEqual(2, GameLost.GetInstance().GetMenu().Length);
    }

    [Test]
    public void TestInitialActiveButtonLost(){
        Assert.AreEqual(0, GameLost.GetInstance().GetActiveMenuButton());
    }

    [Test]
    public void TestNumberOfButtonsWon(){
        Assert.AreEqual(2, GameWon.GetInstance().GetMenu().Length);
    }

    [Test]
    public void TestInitialActiveButtonWon(){
        Assert.AreEqual(0, GameWon.GetInstance().GetActiveMenuButton());
    }

    [Test]
    public void TestNumberOfButtonsPaused(){
        Assert.AreEqual(3, GamePaused.GetInstance().GetMenu().Length);
    }

    [Test]
    public void TestInitialActiveButtonPaused(){
        Assert.AreEqual(0, GamePaused.GetInstance().GetActiveMenuButton());
    }
    [Test]
    public void TestMoveDown(){
        Assert.AreEqual(0, MainMenu.GetInstance().GetActiveMenuButton());
        MainMenu.GetInstance().MoveDown();
        Assert.AreEqual(1, MainMenu.GetInstance().GetActiveMenuButton());
    }


    [Test]
    public void TestMoveUp(){
        Assert.AreEqual(0, MainMenu.GetInstance().GetActiveMenuButton());
        MainMenu.GetInstance().MoveDown();
        Assert.AreEqual(1, MainMenu.GetInstance().GetActiveMenuButton());
        MainMenu.GetInstance().MoveUp();
        Assert.AreEqual(0, MainMenu.GetInstance().GetActiveMenuButton());
    }

    [Test]
    public void TestEdgeDown(){
        Assert.AreEqual(0, MainMenu.GetInstance().GetActiveMenuButton());
        MainMenu.GetInstance().MoveDown();
        MainMenu.GetInstance().MoveDown();
        Assert.AreEqual(1, MainMenu.GetInstance().GetActiveMenuButton());
    }

    [Test]
    public void TestEdgeUp(){
        Assert.AreEqual(0, MainMenu.GetInstance().GetActiveMenuButton());
        MainMenu.GetInstance().MoveUp();
        Assert.AreEqual(0, MainMenu.GetInstance().GetActiveMenuButton());
    }
}