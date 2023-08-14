using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Blocks;


namespace Breakout.PowerUps{
    /// <summary>
    /// Static class responsible for creating a Power Up and rendering the active powerUps
    /// </summary>
    public static class PowerUpHandler{

        private static Entity hardBallEntity;
        private static Entity invicibleEntity;
        private static Entity bigBallEntity;


        private static Vec2F powerUpExtent = new Vec2F(0.045f, 0.03f);


        /// <summary>
        /// Creates a PowerUp and adds it to a container of PowerUps.
        /// </summary>
        /// <param name="powerUps"> The conatiner containing PowerUps moving from blocks towards 
        /// the buttom </param>
        /// <param name="block"> The PowerUpBlock which was destroyed</param>
        public static void CreatePowerUp(EntityContainer<PowerUp> powerUps, PowerUpBlock block){
            switch(block.powerUpType){
                case(PowerUpType.Split):
                    powerUps.AddEntity(new SplitBalls(
                        new DynamicShape(block.CenterPos(powerUpExtent), powerUpExtent),
                        block.powerUpImage
                    ));
                    break;
                case(PowerUpType.Hard_Ball):
                    hardBallEntity = new Entity(
                        new StationaryShape(new Vec2F(0.6f, 0.0f), new Vec2F(0.04f, 0.04f)),
                        block.powerUpImage);
                    powerUps.AddEntity(new HardBalls(
                        new DynamicShape(block.CenterPos(powerUpExtent), powerUpExtent),
                        block.powerUpImage
                    ));
                    break;
                case(PowerUpType.Invincible):
                    invicibleEntity = new Entity(
                        new StationaryShape(new Vec2F(0.65f, 0.0f), new Vec2F(0.04f, 0.04f)),
                        block.powerUpImage);
                    powerUps.AddEntity(new Invincible(
                        new DynamicShape(block.CenterPos(powerUpExtent), powerUpExtent),
                        block.powerUpImage
                    ));
                    break;
                case(PowerUpType.Double_Size):
                    bigBallEntity = new Entity(
                        new StationaryShape(new Vec2F(0.7f, 0.0f), new Vec2F(0.04f, 0.04f)),
                        block.powerUpImage);
                    powerUps.AddEntity(new DoubleSize(
                        new DynamicShape(block.CenterPos(powerUpExtent), powerUpExtent),
                        block.powerUpImage
                    ));
                    break;
                case(PowerUpType.Extra_Life):
                    powerUps.AddEntity(new ExtraLife(
                        new DynamicShape(block.CenterPos(powerUpExtent), powerUpExtent),
                        block.powerUpImage
                    ));
                    break;
                default:
                    break;
            }
        }

        public static void RenderActivePowerUps(List<bool> activePowerUps){
            RenderHardBall(activePowerUps[0]);
            RenderInvincible(activePowerUps[1]);
            RenderBigBall(activePowerUps[2]);
        }

        private static void RenderHardBall(bool hardBall){
            if(hardBall){
                hardBallEntity.RenderEntity();
            }
        }

        private static void RenderInvincible(bool invincible){
            if(invincible){
                invicibleEntity.RenderEntity();
            }
        }

        private static void RenderBigBall(bool bigBalls){
            if(bigBalls){
                bigBallEntity.RenderEntity();
            }
        }

    }

}
