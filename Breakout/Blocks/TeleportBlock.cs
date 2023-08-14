using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Breakout.Blocks{
    /// <summary>
    /// A class for the special block Teleport-block
    /// </summary>
    public class TeleportBlock : Block{
        /// <summary> Deals damage to the block. If the block "survives" it is teleported. 
        ///  Otherwise it is destroyed </summary>
        /// <param name="dmg"></param>
        public override void TakeDamage(int dmg){
            health -= dmg;
            if(health <= 0){
                this.DeleteBlock();
            }
            else{
                this.Teleport();
            }
        }


        /// <summary> Teleports to a new available position </summary>
        /// <param name = "availablePositions"> A list of all the available grid-positions 
        /// on the level </param>
        private void Teleport(){
            if(BlockHandler.availablePositions.Count() != 0){
                System.Random rand = new System.Random();
                int elm = rand.Next(BlockHandler.availablePositions.Count()-1);
                (int x, int y) = BlockHandler.availablePositions[elm];
                BlockHandler.availablePositions.Remove((x,y));
                BlockHandler.availablePositions.Add(BlockHandler.ToGridPos(Shape.Position));
                Vec2F newPos = BlockHandler.ToShapePos((x,y));
                Shape.SetPosition(newPos);
            }
        }

        public TeleportBlock(DynamicShape shape, IBaseImage image) : base(shape, image){
            value = 3;
            this.health = 3*defaultHealth;
        }

    }
}