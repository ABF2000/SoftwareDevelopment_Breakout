using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks{
    /// <summary>
    /// A class for the special block Unbreakable-block
    /// </summary>
    public class UnbreakableBlock : Block{

        /// <summary> Override the TakeDamage-method. This methods essentially does nothing. In this
        /// way the block can never be destroyed by taking damage </summary>
        public override void TakeDamage(int dmg){}
        public UnbreakableBlock(DynamicShape shape, IBaseImage image) : base(shape, image){}
    }
}
