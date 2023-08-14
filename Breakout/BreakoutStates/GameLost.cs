using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.BreakoutMenu;

namespace Breakout.BreakoutStates{

    /// <summary>
    /// Class to handle the game if the game is Lost. Will display the final score and a 
    /// menu the user can navigate.
    /// </summary>
    public class GameLost : Menu, IGameState {
        private static GameLost instance = null;
        public static GameLost GetInstance () {
            if (GameLost.instance == null) {
                GameLost.instance = new GameLost();
                GameLost.instance.InitializeGameState();
            }
            return GameLost.instance;
        }
        private void InitializeGameState() {
            CreateMenu(new List<string> {"Main Menu", "Quit"});
            headLine = new Text("Game Over", new Vec2F(0.35f, 0.3f), new Vec2F(0.4f, 0.5f));
            headLine.SetColor(standardColor);    
        }

        public void RenderState() {
            RenderMenu();
            Score.GameOverScore(false); // Moves the score and changes the color
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if(action == KeyboardAction.KeyPress){
                switch (key){
                    case(KeyboardKey.Up): case(KeyboardKey.W):
                        MoveUp();
                        break;
                    case(KeyboardKey.Down): case(KeyboardKey.S):
                        MoveDown();
                        break;
                    case(KeyboardKey.Enter):
                        switch(activeMenuButton){
                            case(0):
                                SelectButton(new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    ObjectArg1 = MainMenu.GetInstance()
                                });
                                break;
                            case(1):
                                SelectButton(new GameEvent{
                                    EventType = GameEventType.WindowEvent,
                                    Message = "ESCAPE_KEYPRESS"
                                });
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void UpdateState() {}

        public void ResetState() {
            ResetMenu();
        }
    }
}