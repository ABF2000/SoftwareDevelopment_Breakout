using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    /// <summary>
    /// Class for switching between states of the game
    /// </summary>
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            ActiveState = MainMenu.GetInstance();
        }

        private void SwitchState(IGameState state) {
            ActiveState = state;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.GameStateEvent && gameEvent.Message == "CHANGE_STATE"){
                SwitchState((IGameState) gameEvent.ObjectArg1);
                if(gameEvent.StringArg2 == "RESET") ActiveState.ResetState();
            }
        }
    }
}