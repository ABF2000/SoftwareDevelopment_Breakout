using DIKUArcade.Entities;
using DIKUArcade.Graphics;


namespace Breakout.PowerUps{
    /// <summary>
    /// A class for the PowerUp extra life.
    /// </summary>
    public class ExtraLife : PowerUp, IGamePowerUp{
        
        public ExtraLife(DynamicShape shape, IBaseImage image) : base(shape, image){
            powerUpType = PowerUpType.Extra_Life;
        }

        /// <summary>
        /// The effect of the PowerUp. Will try to add one life to the lives-class.
        /// </summary>
        public void GamePowerUp(bool activation){
            Life.ChangeLives(1);
        }

    }
}