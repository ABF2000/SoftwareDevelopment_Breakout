using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Balls;
using DIKUArcade.Math;


namespace Breakout.PowerUps{
    /// <summary>
    /// A class for the PowerUp split balls.
    /// </summary>
    public class SplitBalls : PowerUp, IBallPowerUp{
        public SplitBalls(DynamicShape shape, IBaseImage image) : base(shape, image){
            powerUpType = PowerUpType.Split;
        }

        /// <summary>
        /// The effect of the PowerUp. For each actiove ball it will create two new balls moving 
        /// in a diagonal from the original ball. 
        /// </summary>
        /// <param name="balls"> The balls to be "split". The new balls are added to this 
        /// container as well</param>
        public void BallPowerUp(EntityContainer<Ball> balls, bool activation){
            EntityContainer<Ball> newBalls = new EntityContainer<Ball>();
                balls.Iterate(ballold => {
                    newBalls.AddEntity(new Ball(
                        new DynamicShape(ballold.Shape.Position, ballold.Shape.Extent, 
                                        ballold.Shape.AsDynamicShape().Direction + 
                                                                    new Vec2F(0.01f, 0.0f)),
                        new Image(Path.Combine("Assets", "Images", "ball.png"))));
                    newBalls.AddEntity(new Ball(
                        new DynamicShape(ballold.Shape.Position, ballold.Shape.Extent, 
                                        ballold.Shape.AsDynamicShape().Direction - 
                                                                    new Vec2F(0.01f, 0.0f)),
                        new Image(Path.Combine("Assets", "Images", "ball.png"))));
                });
                newBalls.Iterate(ball => {
                    balls.AddEntity(ball);
                });
                newBalls.ClearContainer();
        }  
    }
}