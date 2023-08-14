using DIKUArcade.Entities;
using Breakout.Balls;

namespace Breakout.PowerUps{
    /// <summary>
    /// Interface for PowerUps which require acces to the ball(s)
    /// </summary>
    public interface IBallPowerUp{

        public void BallPowerUp(EntityContainer<Ball> balls, bool activation);

    }

}