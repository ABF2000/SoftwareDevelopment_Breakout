using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using Breakout.BreakoutStates;


namespace Breakout.PowerUps{
    /// <summary>
    /// A class for the PowerUp invincible.
    /// </summary>
    public class Invincible : PowerUp, IGamePowerUp{
        public Invincible(DynamicShape shape, IBaseImage image) : base(shape, image){
            powerUpType = PowerUpType.Invincible;
        }

        /// <summary>
        /// The effect of the PowerUp. Will set a bool in GameRunning to true when activated. 
        /// If already active it will extend the effect by resetting the timed event.
        /// </summary>
        /// <param name="activation">Bool determining activation or deactivation</param>
        public void GamePowerUp(bool activation){
            if(activation){
                if(!BreakoutBus.GetBus().ResetTimedEvent(3u, TimePeriod.NewSeconds(6))){
                    BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent{
                        EventType = GameEventType.InputEvent,
                        Message = powerUpType.ToString(),
                        StringArg1 = "EXPIRED", 
                        From = this,
                        Id = 3u
                    }, TimePeriod.NewSeconds(6));
                    
                }
            }
            GameRunning.GetInstance().Invincible = activation;
        }

    }


}