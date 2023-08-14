using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace Breakout{
    /// <summary>
    /// Static class which handles the players lives. 
    /// </summary>
    public static class Life{
        private static int livesRemaining = 3; // The player statrts with three lives
        public static int GetLives(){return livesRemaining;} // Used for testing
        private static int maxNumOfLives = 5; // The maximum is 5 lives
        public static bool IsAlive = true;
        private static Vec2F heartExtend = new Vec2F(0.04f, 0.04f);

        private static EntityContainer<Entity> Hearts = new EntityContainer<Entity>();


        /// <summary>
        /// Changes the livesRemaining. Ensures that livesRemaining are above -1 and belove 6 and 
        /// the player is alive when livesRemaining are above 0
        /// </summary>
        /// <param name="life"></param>
        public static void ChangeLives(int life){
            if(livesRemaining + life <= 0){
                livesRemaining = 0;
                IsAlive = false;
            }
            else if(livesRemaining + life < maxNumOfLives){
                livesRemaining += life;
                IsAlive = true;
            }
            else{
                livesRemaining = maxNumOfLives;
                IsAlive = true;
            }
        }

        /// <summary>
        /// Renders lives as images of hearts on the screen. A life is rendered as a red heart and 
        /// otherwise an empty heart is rendered. 5 hearts are always rendered
        /// </summary>
        public static void RenderLives(){
            Hearts.ClearContainer();
            float i = (float)livesRemaining;
            for(int j = 0; j < 5; j++){
                if(i > 0){
                    Hearts.AddEntity(new Entity(
                        new StationaryShape(new Vec2F(0.19f + j*0.04f, 0.0f), heartExtend),
                        new Image(Path.Combine("Assets", "Images", "heart_filled.png"))
                    ));
                    i--;
                }
                else{
                    Hearts.AddEntity(new Entity(
                        new StationaryShape(new Vec2F(0.19f + j*0.04f, 0.0f), heartExtend),
                        new Image(Path.Combine("Assets", "Images", "heart_empty.png"))
                    ));
                }
            }
            Hearts.RenderEntities();
        }

        /// <summary>
        /// Sets the livesRemaining to 3;
        /// </summary>
        public static void ResetLives(){
            ChangeLives(3-livesRemaining);
        }


    }



}