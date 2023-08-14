
namespace Breakout.PowerUps{
    /// <summary>
    /// Interface for PowerUps which requires acces to the player
    /// </summary>
    public interface IPlayerPowerUp{
        public void PlayerPowerUp(Player player, bool activation);

    }

}