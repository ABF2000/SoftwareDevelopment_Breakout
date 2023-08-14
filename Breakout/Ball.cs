using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;


namespace Breakout.Balls{

    /// <summary>
    /// A class for handling the concept of a ball
    /// </summary>
    public class Ball : Entity{

        public DynamicShape shape;

        // Setting a start Direction for the ball
        private Vec2F direction;

        private Random rand;

        // lower bound for the range of possible Direction.X values to be generated when the ball 
        // hits the player
        private float lowerBound = 0.003f;

        // upper bound for the range of possible Direction.X values to be generated when the ball 
        // hits the player
        private float upperBound = 0.007f;

        private float lowerBoundMidCol = -0.001f;

        private float upperBoundMidCol = 0.001f;

        // speed cap for the ball
        private float speedBound = 0.006f;


        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image){
            this.shape = shape;
            this.rand = new Random();
            this.direction = shape.Direction;
        }

        /// <summary> Updates the direction of the shape and moves the shape </summary>
        public void Move(){
            shape.Direction = direction;
            shape.Move();
        }

        /// <summary> Generates a random xDir float </summary>
        public float GenerateXDir(){
            return rand.NextSingle() * (upperBound - lowerBound) + lowerBound;
        }

        public float GenerateXDirMidCollision(){
            return rand.NextSingle() * (upperBoundMidCol - lowerBoundMidCol) + lowerBoundMidCol;
        }

        public bool IsLeftSideBounce(Player player){
            return(this.shape.Position.X >= player.Shape.Position.X && 
                   this.shape.Position.X < player.Shape.Position.X + 0.33f * player.Shape.Extent.X);
        }

        public bool IsRightSideBounce(Player player){
            return(this.shape.Position.X <= player.Shape.Position.X + player.Shape.Extent.X && 
                   this.shape.Position.X > player.Shape.Position.X + 0.66f * player.Shape.Extent.X);
        }

        /// <summary> Method to find the collision direction for the edges of the window </summary>
        public string EdgeBounceDir(){ 
            if (this.shape.Position.X < 0.0f || this.shape.Position.X > 0.97f){
                return "side";
            }
            else if (this.shape.Position.Y > 0.97f){
                return "down";
            }
            else{
                return "null"; // returns null if the ball isn't near a wall, has to return a string 
                               // in the else-condition
            }                 
        }                      

        /// <summary> Method to change the Direction vector after a bounce with an edge of the 
        /// window </summary>
        /// <param name = "bounceDir"> The collision direction represented as a string </param>
        public void EdgeBounce(string bounceDir){ 
            switch(bounceDir){
                case("side"): //right or left edge of the screen
                    this.shape.Direction.X = -1 * this.shape.Direction.X;
                    break;
                case("down"): //top edge of the screen
                    this.shape.Direction.Y = -1 * this.shape.Direction.Y;
                    break;
                default:
                    break;
            }
        }

        /// <summary> Method to change the Direction vector after a bounce with the player 
        /// </summary>
        /// <param name = "player"> The current player object </param>
        public void PlayerBounce(Player player, float randomXDir){
            if (IsLeftSideBounce(player)){
                // multiplying with -1 changes the Direction.Y vector to point in the same direction 
                // as the normal vector of the collision surface
                this.shape.Direction.Y = -1 * this.shape.Direction.Y;

                // implementing a guard clause to cap the speed of the ball
                if (this.shape.Direction.X > -speedBound){

                    this.shape.Direction.X += -randomXDir;
                }
                // normalizing the direction vector so the ball always has the same speed along the 
                // relative direction vector
                this.shape.Direction = Vec2F.Normalize(this.shape.Direction);
            }
            else if (IsRightSideBounce(player)){
                // multiplying with -1 changes the Direction.Y vector to point in the same direction 
                // as the normal vector of the collision surface
                this.shape.Direction.Y = -1 * this.shape.Direction.Y;

                // implementing a guard clause to cap the speed of the ball
                if (this.shape.Direction.X < speedBound){

                    this.shape.Direction.X += randomXDir;
                }

                //normalizing the direction vector so the ball always has the same speed along the 
                // relative direction vector
                this.shape.Direction = Vec2F.Normalize(this.shape.Direction);
            }
            else {
                // multiplying with -1 changes the Direction.Y vector to point in the same direction 
                // as the normal vector of the collision surface
                this.shape.Direction.Y = -1 * this.shape.Direction.Y;

                this.shape.Direction.X += randomXDir;

                this.shape.Direction = Vec2F.Normalize(this.shape.Direction);
            }
        }

        /// <summary> Method to change the Direction vector after a bounce with a block </summary>
        /// <param name = "bounceDir"> The collision direction represented as a struct member 
        /// </param>
        public void BlockBounce(CollisionDirection bounceDir){
            switch(bounceDir){
                case(CollisionDirection.CollisionDirDown):case(CollisionDirection.CollisionDirUp):
                    this.shape.Direction.Y = -1 * this.shape.Direction.Y;
                    break;
                case(CollisionDirection.CollisionDirRight):case(CollisionDirection.CollisionDirLeft):
                    this.shape.Direction.X = -1 * this.shape.Direction.X;
                    break;
                default:
                    break;
            }
        }

    }

}


