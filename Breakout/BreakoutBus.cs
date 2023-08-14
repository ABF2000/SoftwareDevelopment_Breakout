using DIKUArcade.Events;

namespace Breakout {
    /// <summary>
    /// A static class to acces the eventBus. Ensures there only is one eventBus present.
    /// </summary>
    public static class BreakoutBus {
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
        }
    }
}