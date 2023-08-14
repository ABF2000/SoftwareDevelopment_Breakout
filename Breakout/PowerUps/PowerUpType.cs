
namespace Breakout.PowerUps{
    /// <summary>
    /// Enum containing the different possible types of PowerUps
    /// </summary>
    public enum PowerUpType{
        Extra_Life,
        Invincible,
        Double_Size,
        Split,
        Hard_Ball,
    }

    /// <summary>
    /// Small static class which maps each type from above to a string of the corresponding image
    /// name
    /// </summary>
    public static class PowerUpExtension{
        public static string toImageString(PowerUpType power){
            switch(power){
                case PowerUpType.Extra_Life:
                    return "LifePickUp.png";
                case PowerUpType.Invincible:
                    return "WallPowerUp.png";
                case PowerUpType.Double_Size:
                    return "BigPowerUp.png";
                case PowerUpType.Split:
                    return "SplitPowerUp.png";
                case PowerUpType.Hard_Ball:
                    return "DamagePickUp.png";
                default:
                    return "minsk-square.png";
            }
        }
    }
}