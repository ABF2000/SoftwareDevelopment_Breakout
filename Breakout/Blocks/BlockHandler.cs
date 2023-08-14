using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.LevelLoading;


namespace Breakout.Blocks{

    /// <summary>
    /// Static class responsible for creating the formation of blocks and. It also contains 
    /// utility-methods and a list containing every position not occupied by a block
    /// </summary>
    public static class BlockHandler{ 
        
        private static Vec2F extent = new Vec2F(0.083f, 0.04f);
        private static float hoirsontalSpace = 12.0f; // Space for 12 blocks on a row
        private static float verticalSpace = 25.0f; // Space for 25 blocks in a collumn
        private static int lowestBlockPosition = 15; // Lowest possible position for a block
        public static List<(int,int)> availablePositions {get; set;}

        /// <summary> Converts a Vec2F-position to a (int,int) grid-position. The grid-position is
        /// needed for keeping track of available positions on the board </summary>
        /// <param name="pos"> The positions to be converted </param>
        public static (int,int) ToGridPos(Vec2F pos){
            return (((int)Math.Round(((1.0f - extent.X - pos.X)*hoirsontalSpace)), 
                    (int)Math.Round(((1.0f - pos.Y)*verticalSpace))));
        }

        /// <summary> Converts a Grid-position to a Vec2F-position. The Vec2F-position is needed
        /// when giving an entities Shape a position </summary>
        /// <param name="pos"> The positions to be converted </param>
        public static Vec2F ToShapePos((int,int) grid){ 
            (int x, int y) = grid;
            return (new Vec2F(1.0f - extent.X - ((float)x) / hoirsontalSpace, 
                    1.0f - ((float)y) / verticalSpace ));
        }

        /// <summary> Transform the data from a map file to formation of different types of Blocks. 
        /// If a block has no attribute it becomes a default block. </summary>
        /// <param name = "mapdata"> List that specifies the layout of the map </param>
        /// <param name = "blockFormationContainer"> List that details the blocks to be utilized
        /// </param>
        public static void CreateBlockFormation(List<FormationData> mapdata, 
                            EntityContainer<Block> blockFormationContainer){
            foreach (FormationData block in mapdata){
                switch(block.Attribute){
                    case("Teleport"):
                        blockFormationContainer.AddEntity(new TeleportBlock(
                            new DynamicShape(ToShapePos(block.GridPos), extent),
                            new Image(Path.Combine("Assets", "Images", block.Image))
                        ));
                        break;

                    case("Hardened"):
                        blockFormationContainer.AddEntity(new HardenedBlock(
                            new DynamicShape(ToShapePos(block.GridPos), extent),
                            new Image(Path.Combine("Assets", "Images", block.Image)),
                            new Image(Path.Combine("Assets", "Images", 
                                block.Image[..(block.Image.Length-4)]+"-damaged.png"))
                        ));
                        break;

                    case("Unbreakable"):
                        blockFormationContainer.AddEntity(new UnbreakableBlock(
                            new DynamicShape(ToShapePos(block.GridPos), extent),
                            new Image(Path.Combine("Assets", "Images", block.Image))                            
                        ));
                        break;
                    case("PowerUp"):
                        blockFormationContainer.AddEntity(new PowerUpBlock(
                            new DynamicShape(ToShapePos(block.GridPos), extent),
                            new Image(Path.Combine("Assets", "Images", block.Image))                            
                        ));
                        break;

                    default:
                        blockFormationContainer.AddEntity(new Block(
                            new DynamicShape(ToShapePos(block.GridPos), extent),
                            new Image(Path.Combine("Assets", "Images", block.Image))
                        ));
                        break;
                }
            }
            InitializeAvailablePositions(mapdata);
        }


        /// <summary> Initializes a new list of available grid-positions. </summary>
        /// <param name="map">A list containing information of blocks positions</param>
        private static void InitializeAvailablePositions(List<FormationData> map){
            availablePositions = new List<(int,int)>();
            for(int i = 0; i < (int)hoirsontalSpace; i++){
                for(int j = 1; j < lowestBlockPosition+1; j++){
                    availablePositions.Add((i,j));
                }
            }
            foreach(FormationData element in map){
                if(availablePositions.Contains(element.GridPos)){
                    availablePositions.Remove(element.GridPos);
                }
            }  
        }
        
    }
}