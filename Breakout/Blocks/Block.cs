using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Breakout.Blocks{

    /// <summary>
    /// Super class which every block inherits from. Also the default-block.
    /// </summary>
    public class Block : Entity{

        protected const int defaultHealth = 1;
        protected const int defaultValue = 1;
        protected int value;
        protected int health;


        public int GetHealth(){ return health; } // For testing
        public int GetValue(){ return value; } // For testing

        public virtual void TakeDamage(int dmg){
            health -= dmg;
            if(health <= 0){
                this.DeleteBlock();
            }
        }

        /// <summary>
        /// Delete the entity and add position to list of positions not occupied by a block
        /// </summary>
        public virtual void DeleteBlock(){
            BlockHandler.availablePositions.Add(BlockHandler.ToGridPos(Shape.Position));
            this.DeleteEntity();
        }

        public Block(DynamicShape shape, IBaseImage image) : base(shape, image){
            this.health = defaultHealth;
            this.value = defaultValue;
        }

        /// <summary>
        /// Finds central position of the block with respect to some extent
        /// </summary>
        public Vec2F CenterPos(Vec2F extent){
            return new Vec2F(
                Shape.Position.X + Shape.Extent.X/2 - extent.X/2,
                Shape.Position.Y + Shape.Extent.Y/2 - extent.Y/2
            );
        }

        public virtual new void RenderEntity(){
            Image.Render(Shape);
        }

    }
}
