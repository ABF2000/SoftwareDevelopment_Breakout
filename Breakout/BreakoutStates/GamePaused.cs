using DIKUArcade.State;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using Breakout.BreakoutMenu;


namespace Breakout.BreakoutStates{
    /// <summary>
    /// Class to handle the game if the game is paused. Will display a 
    /// menu the user can navigate.
    /// </summary>
public class GamePaused : Menu, IGameState {
        private static GamePaused instance = null;
        public static GamePaused GetInstance () {
            if (GamePaused.instance == null) {
                GamePaused.instance = new GamePaused();
                GamePaused.instance.InitializeGameState();
            }
            return GamePaused.instance;
        }


        private void InitializeGameState() {
            CreateMenu(new List<string> {"Continue", "Restart Game", "Return to Menu"});      

            backGroundImage = new (
                new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));

            headLine = new Text("Game Paused", new Vec2F(0.35f, 0.3f), new Vec2F(0.4f, 0.5f));
            headLine.SetColor(standardColor);

        }

        public void RenderState() {
            RenderMenu();    
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
                                    ObjectArg1 = GameRunning.GetInstance()
                                });
                                break;
                            case(1):
                                SelectButton(new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    StringArg2 = "RESET",
                                    ObjectArg1 = GameRunning.GetInstance()
                                });
                                break;
                            case(2):
                                SelectButton(new GameEvent{
                                    EventType = GameEventType.GameStateEvent,
                                    Message = "CHANGE_STATE",
                                    ObjectArg1 = MainMenu.GetInstance()
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