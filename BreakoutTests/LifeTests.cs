using NUnit.Framework;
using Breakout;

namespace BreakoutTests;

public class LifeTests{
    [SetUp]
    public void SetupLifeTest(){
        Life.ResetLives();
    }


    [Test]
    public void TestInitialState(){
        Assert.AreEqual(3, Life.GetLives());
        Assert.True(Life.IsAlive);
    }

    [Test]
    public void TestResetLives(){
        Life.ChangeLives(-5);
        Assert.AreEqual(0, Life.GetLives());
        Assert.False(Life.IsAlive);
        Life.ResetLives();
        Assert.AreEqual(3, Life.GetLives());
        Assert.True(Life.IsAlive);
    }

    [Test]
    public void TestRemoveLife(){
        var prevLives = Life.GetLives();
        Life.ChangeLives(-1);
        Assert.AreEqual(prevLives-1, Life.GetLives());
    }

    [Test]
    public void TestAddLife(){
        var prevLives = Life.GetLives();
        Life.ChangeLives(1);
        Assert.AreEqual(prevLives+1, Life.GetLives());
    }

    [Test]
    public void TestMaxLife(){
        Life.ChangeLives(3);
        Assert.AreEqual(5, Life.GetLives());
    }

    [Test]
    public void TestZeroLives(){
        Assert.True(Life.IsAlive);
        Life.ChangeLives(-5);
        Assert.AreEqual(0, Life.GetLives());
        Assert.False(Life.IsAlive);
    }
}