using DIKUArcade.Math;
using DIKUArcade.Graphics;


namespace Breakout{
    /// <summary>
    /// Static class to handle the timer on certain levels. The class will keep track of 
    /// time and render the time remaining for the user
    /// </summary>
    public static class Timer{
        
        private static long timeOfCreation = 0;
        private static long metaTime;
        public static long GetMetaTime(){return metaTime;}
        private static bool timeIsPresent;
        public static bool IsZero;
        private static Vec2F position = new Vec2F(0.0f, -0.255f); 

        private static Vec2F extent = new Vec2F(0.3f, 0.3f);
        private static Text display = new Text((metaTime).ToString(), 
                                      position, 
                                      extent);

        private static int displayTime;
        public static int GetDisplayTime(){return displayTime;}



        public static void RenderTimer(){
            if(timeIsPresent){
                display.SetText("Time: " + displayTime.ToString());
                display.SetColor(new Vec3I(255, 255, 0));
                display.RenderText();
            }
        }

        /// <summary>
        /// Sets the timer to some value to count down from
        /// </summary>
        public static void SetTimer(long time){
            if(time >= 0){
                metaTime = time;
                RestartTimer();
                timeIsPresent = true;
            }
            else{
                RestartTimer();
            }
        }

        public static void UpdateTimeOfCreation(){
            timeOfCreation = DIKUArcade.Timers.StaticTimer.GetElapsedMilliseconds();
        }

        public static bool IsSecondElapsed(){
            if (timeOfCreation + 1000 < DIKUArcade.Timers.StaticTimer.GetElapsedMilliseconds()){
                UpdateTimeOfCreation();
                return true;
            }
            else{
                return false;
            }
        }

        /// <summary>
        /// Updates the timer. If a second has elapsed the displayed time goes down by 1
        /// </summary>
        public static void UpdateTimer(){
            if (IsSecondElapsed()){
                displayTime = (int)Math.Round(metaTime - 
                                            DIKUArcade.Timers.StaticTimer.GetElapsedSeconds());
                if (displayTime <= 0){
                    IsZero = true;
                }

            }
        }

        public static void RestartTimer(){
            timeOfCreation = 0;
            displayTime = (int)metaTime;
            IsZero = false;
            timeIsPresent = false;
            DIKUArcade.Timers.StaticTimer.RestartTimer();
        }

        public static void PauseTimer(){
            DIKUArcade.Timers.StaticTimer.PauseTimer();
        }

        public static void ResumeTimer(){
            DIKUArcade.Timers.StaticTimer.ResumeTimer();
        }

    }
    
}
