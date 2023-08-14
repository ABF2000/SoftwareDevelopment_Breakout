using NUnit.Framework;
using DIKUArcade.Events;
using Breakout;
using System.Collections.Generic;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.IO;
using Breakout.PowerUps;
using Breakout.Balls;
using Breakout.BreakoutStates;
using DIKUArcade.Timers;
using Breakout.Blocks;


namespace BreakoutTests;
public class PowerUpTests{

    private DoubleSize doubleSize;
    private HardBalls hardBalls;
    private Invincible invincible;
    private ExtraLife extraLife;
    private SplitBalls splitBalls;
    private PowerUp powerUp;
    private EntityContainer<Ball> balls;
    private EntityContainer<PowerUp> powerUps;

    private PowerUpBlock powerUpBlock;

    [SetUp]
    public void setup(){
        DIKUArcade.GUI.Window.CreateOpenGLContext();
        
        try{
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { 
                                                GameEventType.WindowEvent, 
                                                GameEventType.PlayerEvent,  
                                                GameEventType.GameStateEvent, 
                                                GameEventType.InputEvent 
                                                });
        }catch(System.InvalidOperationException){}

        GameRunning.GetInstance().ResetState();



        balls = new EntityContainer<Ball>(10);
        balls.AddEntity(new Ball(
            new DynamicShape(new Vec2F(0.07f, 0.07f), new Vec2F(0.03f, 0.03f), new Vec2F(0.0f, -0.01f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")))
        );
        
        doubleSize = new DoubleSize(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", PowerUpExtension.toImageString(PowerUpType.Double_Size)))
        );
        hardBalls = new HardBalls(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", PowerUpExtension.toImageString(PowerUpType.Hard_Ball)))
        );
        extraLife = new ExtraLife(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", PowerUpExtension.toImageString(PowerUpType.Extra_Life)))
        );
        invincible = new Invincible(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", PowerUpExtension.toImageString(PowerUpType.Invincible)))
        );
        splitBalls = new SplitBalls(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", PowerUpExtension.toImageString(PowerUpType.Split)))
        );
        powerUp = new PowerUp(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", PowerUpExtension.toImageString(PowerUpType.Split)))
        );

        powerUps = new EntityContainer<PowerUp>(10);
        powerUpBlock = new PowerUpBlock(
            new DynamicShape(BlockHandler.ToShapePos((5,5)), new Vec2F(0.083f, 0.04f)),
            new Image(Path.Combine("Assets", "Images", "blue-block.png"))
        );

        PowerUpHandler.CreatePowerUp(powerUps, powerUpBlock);

    }


    [Test]
    public void TestBigBallType(){
        Assert.AreEqual(doubleSize.GetPowerUpType(), PowerUpType.Double_Size);
    }

    [Test]
    public void TestBigBallActivation(){
        Assert.False(GameRunning.GetInstance().BigBall);
        float xOld = 0;
        float yOld = 0;
        balls.Iterate(ball => {
            xOld = ball.Shape.Extent.X;
            yOld = ball.Shape.Extent.Y;
        });

        doubleSize.BallPowerUp(balls, true);
        Assert.True(BreakoutBus.GetBus().ResetTimedEvent(1u, TimePeriod.NewSeconds(6)));

        float xNew = 0;
        float yNew = 0;
        balls.Iterate(ball => {
            xNew = ball.Shape.Extent.X;
            yNew = ball.Shape.Extent.Y;
        });
        Assert.True(GameRunning.GetInstance().BigBall);
        Assert.AreEqual(2*xOld, xNew);
        Assert.AreEqual(2*yOld, yNew);
    }

    [Test]
    public void TestBigBallDeactivation(){
        doubleSize.BallPowerUp(balls, true);
        Assert.True(GameRunning.GetInstance().BigBall);
        float xOld = 0;
        float yOld = 0;
        balls.Iterate(ball => {
            xOld = ball.Shape.Extent.X;
            yOld = ball.Shape.Extent.Y;
        });


        doubleSize.BallPowerUp(balls, false);

        float xNew = 0;
        float yNew = 0;
        balls.Iterate(ball => {
            xNew = ball.Shape.Extent.X;
            yNew = ball.Shape.Extent.Y;
        });

        Assert.False(GameRunning.GetInstance().BigBall);
        Assert.AreEqual(0.5f*xOld, xNew);
        Assert.AreEqual(0.5f*yOld, yNew);

    }


    [Test]
    public void TestHardBallType(){
        Assert.AreEqual(hardBalls.GetPowerUpType(), PowerUpType.Hard_Ball);
    }


    [Test]
    public void TestHardBallActivation(){
        Assert.False(GameRunning.GetInstance().HardBall);

        hardBalls.ActivatePowerUp();
        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.True(BreakoutBus.GetBus().ResetTimedEvent(2u, TimePeriod.NewSeconds(6)));

        Assert.True(GameRunning.GetInstance().HardBall);

    }

    [Test]
    public void TestHardBallDeactivation(){
        hardBalls.ActivatePowerUp();
        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.True(GameRunning.GetInstance().HardBall);
        
        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
            EventType = GameEventType.InputEvent,
            Message = "Hard_Ball",
            StringArg1 = "EXPIRED", 
            From = hardBalls,
            Id = 2u
        });
        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.False(GameRunning.GetInstance().HardBall);
    }



    [Test]
    public void TestExtraLifeType(){
        Assert.AreEqual(extraLife.GetPowerUpType(), PowerUpType.Extra_Life);
    }

    [Test]
    public void TestExtraLifeActivation(){
        int lifeOld = Life.GetLives();

        extraLife.ActivatePowerUp();
        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.AreEqual(lifeOld+1, Life.GetLives());

    }


    [Test]
    public void TestInvincibleType(){
        Assert.AreEqual(invincible.GetPowerUpType(), PowerUpType.Invincible);
    }

    [Test]
    public void TestInvincibleActivation(){
        Assert.False(GameRunning.GetInstance().Invincible);

        invincible.ActivatePowerUp();
        BreakoutBus.GetBus().ProcessEventsSequentially();
        Assert.True(BreakoutBus.GetBus().ResetTimedEvent(3u, TimePeriod.NewSeconds(6)));

        Assert.True(GameRunning.GetInstance().Invincible);
    }

    [Test]
    public void TestInvincibleDeactivation(){
        invincible.ActivatePowerUp();
        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.True(GameRunning.GetInstance().Invincible);
        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
            EventType = GameEventType.InputEvent,
            Message = "Invincible",
            StringArg1 = "EXPIRED", 
            From = invincible,
            Id = 3u
        });
        BreakoutBus.GetBus().ProcessEventsSequentially();

        Assert.False(GameRunning.GetInstance().Invincible);
    }


    [Test]
    public void TestSplitBallsType(){
        Assert.AreEqual(splitBalls.GetPowerUpType(), PowerUpType.Split);
    }


    [Test]
    public void TestSplitBallsActivation(){
        int numberOfBalls = 0;
        numberOfBalls = balls.CountEntities();
        Assert.AreEqual(1, numberOfBalls);

        splitBalls.BallPowerUp(balls, true);
        numberOfBalls = balls.CountEntities();

        Assert.AreEqual(3, numberOfBalls);

    }

    [Test]
    public void TestMovePowerUp(){
        float oldX = 0;
        float oldY = 0;

        oldX = powerUp.Shape.Position.X;
        oldY = powerUp.Shape.Position.Y;

        powerUp.Move();

        float newX = 0;
        float newY = 0;

        newX = powerUp.Shape.Position.X;
        newY = powerUp.Shape.Position.Y;

        Assert.AreEqual(oldX, newX);
        Assert.AreEqual(oldY-0.007f, newY);
    }

    [Test]
    public void TestMovePowerUpOut(){
        Assert.AreEqual(1, powerUps.CountEntities());

        for(int i = 0; i < 200; i++){
            powerUps.Iterate(powerUp => {
                powerUp.Move();
            });
        }
        Assert.AreEqual(0, powerUps.CountEntities());
    }
}






