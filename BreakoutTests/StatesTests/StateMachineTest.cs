using NUnit.Framework;
using Breakout;
using DIKUArcade.State;
using Breakout.BreakoutStates;
using DIKUArcade.Events;


namespace BreakoutTests;


public class StateMachineTest{
    private StateMachine stateMachine;

    [SetUp]
    public void SetupStateMachineTest(){
        stateMachine = new StateMachine();
    }

    [Test]
    public void TestInitialState(){
        Assert.AreEqual(MainMenu.GetInstance(), stateMachine.ActiveState);
    }

    [Test]
    public void TestChangeToGameRunning(){
        Assert.AreEqual(MainMenu.GetInstance(), stateMachine.ActiveState);
        stateMachine.ProcessEvent(new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            StringArg2 = "RESET",
            ObjectArg1 = GameRunning.GetInstance()
        });
        Assert.AreEqual(GameRunning.GetInstance(), stateMachine.ActiveState);
    }

    [Test]
    public void TestChangeToGamePaused(){
        Assert.AreEqual(MainMenu.GetInstance(), stateMachine.ActiveState);
        stateMachine.ProcessEvent(new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            ObjectArg1 = GamePaused.GetInstance()
        });
        Assert.AreEqual(GamePaused.GetInstance(), stateMachine.ActiveState);
    }

    [Test]
    public void TestChangeToGameWon(){
        Assert.AreEqual(MainMenu.GetInstance(), stateMachine.ActiveState);
        stateMachine.ProcessEvent(new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            ObjectArg1 = GameWon.GetInstance()
        });
        Assert.AreEqual(GameWon.GetInstance(), stateMachine.ActiveState);
    }

    [Test]
    public void TestChangeToGameLost(){
        Assert.AreEqual(MainMenu.GetInstance(), stateMachine.ActiveState);
        stateMachine.ProcessEvent(new GameEvent{
            EventType = GameEventType.GameStateEvent,
            Message = "CHANGE_STATE",
            ObjectArg1 = GameLost.GetInstance()
        });
        Assert.AreEqual(GameLost.GetInstance(), stateMachine.ActiveState);
    }
}