using NUnit.Framework;
using Breakout;

namespace BreakoutTests;

public class TimerTests{
    [SetUp]
    public void SetupTimerTest(){
        Timer.SetTimer(0);
    }


    [Test]
    public void TestSetValidTimer(){
        Assert.AreEqual(0, Timer.GetMetaTime());
        Timer.SetTimer(180);
        Assert.AreEqual(180, Timer.GetMetaTime());
    }

    [Test]
    public void TestSetInvalidTimer(){
        Assert.AreEqual(0, Timer.GetMetaTime());
        Timer.SetTimer(-180);
        Assert.AreEqual(0, Timer.GetMetaTime());
    }

    [Test]
    public void TestSecondElapsedFalse(){
        Assert.False(Timer.IsSecondElapsed());
    }

    [Test]
    public void TestIsZeroFalse(){
        Assert.False(Timer.IsZero);
    }
}