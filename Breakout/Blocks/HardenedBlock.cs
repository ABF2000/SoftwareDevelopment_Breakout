using DIKUArcade.Entities;
using DIKUArcade.Graphics;


namespace Breakout.Blocks{

    /// <summary>
    /// A class for the special block hardened-block
    /// </summary>
    public class HardenedBlock : Block{
        private IBaseImage alternativeImage;
        private bool damaged = false;

        /// <summary> Overrides the virtual method to change the way this specific block takes damage </summary>
        /// <param name = "dmg"> The amount of damage the block takes </param>
        public override void TakeDamage(int dmg)
        {
            health -= dmg;
            if(health <= defaultHealth && !damaged){
                this.Image = this.alternativeImage;
                damaged = true;
            }
            if(health <= 0) {
                this.DeleteBlock();
            }
        }

        public HardenedBlock(DynamicShape shape, IBaseImage image, IBaseImage altImage) 
                            : base(shape, image){
            this.health = 2*defaultHealth;  // Has double health and value
            this.value = 2*defaultValue;
            this.alternativeImage = altImage; // For when the block is half destroyed
        }
    }
}