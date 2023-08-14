using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.BreakoutMenu;

namespace Breakout.BreakoutStates{
    /// <summary>
    /// Class to handle the game when in the main menu. Will display a 
    /// menu the user can navigate.
    /// </summary>
    public class MainMenu : Menu, IGameState {
        private static MainMenu instance = null;

        public static MainMenu GetInstance () {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }
        /// <summary> Initializes the gamestate with the necessary classes, variables and such 
        /// </summary>
        private void InitializeGameState() {
            CreateMenu(new List<string> {"New Game", "Quit"});

            headLine = new Text("Main Menu", new Vec2F(0.35f, 0.3f), new Vec2F(0.4f, 0.5f));
            headLine.SetColor(standardColor);

        }

        /// <summary> Calls all the specified render-functions </summary>
        public void RenderState() {
            RenderMenu();
        }

        /// <summary> Performs actions depending on the received KeyboardAction </summary>
        /// <param name = "action"> Keyrelase and Keypress represented in an enum </param>
        /// <param name = "key"> All keyboard keys represented in an enum </param>
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
                                    StringArg2 = "RESET",
                                    ObjectArg1 = GameRunning.GetInstance()
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