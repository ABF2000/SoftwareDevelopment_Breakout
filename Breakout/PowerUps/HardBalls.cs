using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.Balls;
using Breakout.BreakoutStates;

namespace Breakout.PowerUps{
    /// <summary>
    /// A class for the PowerUp hard ball.
    /// </summary>
    public class HardBalls : PowerUp, IBallPowerUp{
        public HardBalls(DynamicShape shape, IBaseImage image) : base(shape, image){
            powerUpType = PowerUpType.Hard_Ball;
        }

        /// <summary>
        /// The effect of the PowerUp. Will set a bool in GameRunning to true when activated. 
        /// If already active it will extend the effect by resetting the timed event.
        /// </summary>
        /// <param name="activation">Bool determining activation or deactivation</param>
        public void BallPowerUp(EntityContainer<Ball> balls,  bool activation){
            if(activation){
                if(!BreakoutBus.GetBus().ResetTimedEvent(2u, TimePeriod.NewSeconds(6))){
                    BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                        EventType = GameEventType.InputEvent,
                        Message = powerUpType.ToString(),
                        StringArg1 = "EXPIRED", 
                        From = this,
                        Id = 2u
                    }, TimePeriod.NewSeconds(6));    
                }
            }
            GameRunning.GetInstance().HardBall = activation;
        }

    }

}