using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;

namespace Breakout.PowerUps{
    /// <summary>
    /// Class for PowerUps to inherit from. 
    /// </summary>
    public class PowerUp : Entity{
        protected Vec2F Speed = new Vec2F(0.0f, -0.007f);

        protected PowerUpType powerUpType; 
        public PowerUpType GetPowerUpType(){return powerUpType;} // Used for testing

        public PowerUp(DynamicShape shape, IBaseImage image) : base(shape, image){
            Shape.AsDynamicShape().ChangeDirection(Speed);
        }

        /// <summary>
        /// Activates the PowerUp by creating an event with the object and type of PowerUp
        /// </summary>
        public void ActivatePowerUp(){
            BreakoutBus.GetBus().RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.InputEvent, 
                    Message = powerUpType.ToString(),
                    StringArg1 = "ACTIVATED",
                    From = this,
                });        
        }


        /// <summary>
        /// Moves the PowerUp and deletes it if it reaches the buttom
        /// </summary>
        public void Move(){
            Shape.Move();
            if(Shape.Position.Y <= 0.0f){
                DeleteEntity();
            }
        }
    }
}