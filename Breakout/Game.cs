
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using Breakout.BreakoutStates;
using DIKUArcade.Events;

namespace Breakout{
    /// <summary>
    /// The class to run every state. The specific states controls how the game appears
    /// </summary>
    public class Game : DIKUGame, IGameEventProcessor{
        private StateMachine stateMachine;

        private void KeyHandler(KeyboardAction action, KeyboardKey key){
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }

        public Game(WindowArgs windowArgs) : base(windowArgs){
            BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType> { 
                                                    GameEventType.WindowEvent, 
                                                    GameEventType.PlayerEvent,  
                                                    GameEventType.GameStateEvent, 
                                                    GameEventType.InputEvent 
                                                    });
            stateMachine = new StateMachine();
            window.SetKeyEventHandler(KeyHandler);
            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) { 
                    case("ESCAPE_KEYPRESS"):
                        window.CloseWindow();
                        break;
                    default:
                        break;
                } 
            }
        }

        public override void Render(){
            stateMachine.ActiveState.RenderState();
        }

        public override void Update(){
            BreakoutBus.GetBus().ProcessEventsSequentially();
            stateMachine.ActiveState.UpdateState();
        }

    }
}