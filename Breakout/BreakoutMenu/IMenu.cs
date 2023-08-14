using DIKUArcade.Events;

namespace Breakout.BreakoutMenu{

    /// <summary>
    /// Interface for creating a simple list-menu
    /// </summary>
    public interface IMenu{
        void CreateMenu(List<string> buttons);

        void ResetMenu();

        void RenderMenu();

        void MoveDown();

        void MoveUp();

        void SelectButton(GameEvent gameEvent);

    }
}