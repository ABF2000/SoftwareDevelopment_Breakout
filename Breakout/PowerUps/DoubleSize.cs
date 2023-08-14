using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Balls;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using Breakout.BreakoutStates;


namespace Breakout.PowerUps{
    /// <summary>
    /// A class for the PowerUp double size.
    /// </summary>
    public class DoubleSize : PowerUp, IBallPowerUp{
        
        public DoubleSize(DynamicShape shape, IBaseImage image) : base(shape, image){
            powerUpType = PowerUpType.Double_Size;
        }

        /// <summary>
        /// The effect of the PowerUp. It double the size of every ball currently in play. 
        /// If already active it will extend the effect by resetting the timed event.
        /// </summary>
        /// <param name="balls"> The balls to be affected </param>
        /// <param name="activation"> Bool determining activation or deactivation
        /// If true the balls will double in size. If false the balls will be halfed 
        /// in sized (returned to normal size)</param>
        public void BallPowerUp(EntityContainer<Ball> balls, bool activation){
            if(activation){
                if(!BreakoutBus.GetBus().ResetTimedEvent(1u, TimePeriod.NewSeconds(6))){
                    BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                        EventType = GameEventType.InputEvent,
                        Message = powerUpType.ToString(),
                        StringArg1 = "EXPIRED", 
                        From = this,
                        Id = 1u
                    }, TimePeriod.NewSeconds(6));
                    balls.Iterate(ball => {
                        ball.Shape.Extent *= 2.0f;
                    });
                }
            }
            else{
                balls.Iterate(ball => {
                    ball.Shape.Extent *= 0.5f;
                });
            }
            GameRunning.GetInstance().BigBall = activation;
        }
    }
}