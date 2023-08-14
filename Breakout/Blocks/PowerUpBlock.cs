using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.PowerUps;
using DIKUArcade.Math;

namespace Breakout.Blocks{
    /// <summary>
    /// A class for the special block PowerUp-block
    /// </summary>
    public class PowerUpBlock : Block{
        private Random rand = new Random();
        public Image powerUpImage {get; private set;}
        private Vec2F powerUpExtent = new Vec2F(0.045f, 0.03f);

        public PowerUpType powerUpType {get; private set;}

        public PowerUpBlock(DynamicShape shape, IBaseImage image) : base(shape, image){
            ChoosePowerUp();
        }

        /// <summary>
        /// Generates a random PowerUp from the PowerUpType-enum and saves the type in the block.
        /// It also genrates an image of the Power Up
        /// </summary>
        private void ChoosePowerUp(){
            Type type = typeof(PowerUpType);
            Array powerUps = type.GetEnumValues();
            int value = rand.Next(powerUps.Length);

            powerUpType = (PowerUpType)powerUps.GetValue(value);
            powerUpImage = new Image(Path.Combine("Assets", "Images", 
                                        PowerUpExtension.toImageString(powerUpType)));

        }

        /// <summary>
        /// Renders the block and the PowerUp-image as a smaller version within the block
        /// </summary>
        public override void RenderEntity(){
            base.RenderEntity();
            powerUpImage.Render(new StationaryShape(CenterPos(powerUpExtent), powerUpExtent));
        }

    }

}