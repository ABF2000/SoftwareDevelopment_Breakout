using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout{
    /// <summary>
    /// Class for rendering and updating the score
    /// </summary>
    public static class Score{
        private static int points = 0;
        public static int GetPoints(){return points;}
        private static Vec2F scorePosition = new Vec2F(0.45f, -0.165f);
        private static Vec2F scoreExtent = new Vec2F(0.3f, 0.2f);
        private static Vec2F backgroundExtent = new Vec2F(0.2f, 0.04f);
        private static Vec2F backgroundPosition = new Vec2F(0.4f, 0.0f);
        private static Text display = new Text(points.ToString(), scorePosition, scoreExtent);
        public static Text GetDisplay() {return display;}
        private static Entity background = new Entity(
            new StationaryShape(backgroundPosition, backgroundExtent),
            new Image(Path.Combine("Assets", "Images", "emptyPoint.png"))
        );
        

        /// <summary> Adds points </summary>
        /// <param name = "Point"> The amount of points to be awarded for destroying a block as an 
        /// int </param>
        public static void AddPoints(int Point){
            points += Point;
        }

        /// <summary> Renders the points in game window </summary>
        public static void RenderPoints(){
            background.RenderEntity();
            display.SetText(points.ToString());
            display.RenderText();
        }


        /// <summary> Resets current points to 0 </summary>
        public static void ResetPoints(){
            points = 0;
            display = new Text(points.ToString(), scorePosition, scoreExtent);
            display.SetColor(new Vec3I(255, 255, 0));
        }

        /// <summary>
        /// Renders the score differently if the game is over. The score becomes green if won and 
        /// red if lost. The score moves to a central position
        /// </summary>
        /// <param name="gameWon"> Bool stating if the game was won(true) or lost(false)</param>
        public static void GameOverScore(bool gameWon){
            display.SetText("Score: " + points.ToString());
            if(gameWon){
                display.SetColor(new Vec3I(0, 255, 0));
            }else{
                display.SetColor(new Vec3I(255, 0, 0));
            }
            display.GetShape().Position = new Vec2F(0.35f, 0.2f);
            display.GetShape().Extent = new Vec2F(0.4f, 0.5f);
            display.RenderText();
        }
    }
}