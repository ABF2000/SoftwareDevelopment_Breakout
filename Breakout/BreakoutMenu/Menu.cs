using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Math;
using Breakout.BreakoutStates;


namespace Breakout.BreakoutMenu{

    /// <summary>
    /// Class implementing the IMenu-interface. Contains methods for creating and navigating a 
    /// basic menu
    /// </summary>
    public class Menu : IMenu{
        protected Entity backGroundImage;
        protected Text headLine;
        protected Text[] menuButton;
        public Text[] GetMenu(){return menuButton;}
        protected int activeMenuButton = 0;
        public int GetActiveMenuButton(){return activeMenuButton;}
        protected int maxMenuButtons;
        protected Vec3I standardColor = new Vec3I(170, 100, 50);
        protected Vec3I selectedColor = new Vec3I(45, 197, 252);


        /// <summary>
        /// Creates a menu and saves each entry in the array "menuButton"
        /// </summary>
        /// <param name="buttons"> A list of the names of the buttons to be created </param>
        public void CreateMenu(List<string> buttons){
            backGroundImage = new (
                new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f),
                new Image(Path.Combine("Assets", "Images", "shipit_titlescreen.png")));

            maxMenuButtons = buttons.Count;
            menuButton = new Text[maxMenuButtons];
            int i = 0;
            foreach(string button in buttons){
                menuButton[i] = new Text(button, 
                                        new Vec2F(0.35f, (float)(maxMenuButtons-1-i)/10.0f), 
                                        new Vec2F(0.4f, 0.5f));
                i++;
            }
            ResetMenu(); 
        }        

        /// <summary>
        /// Method for setting the first button as the selected button and eve
        /// </summary>
        public void ResetMenu(){   
            activeMenuButton = 0;
            menuButton[0].SetColor(selectedColor);
            for(int i = 1; i < menuButton.Length; i++){
                menuButton[i].SetColor(standardColor);
            } 
        }

        public void RenderMenu(){
            backGroundImage.RenderEntity();
            headLine.RenderText();

            foreach (Text button in menuButton) {
                button.RenderText();
            }           
        }

        public void MoveDown(){
            if(activeMenuButton != maxMenuButtons-1){
                menuButton[activeMenuButton].SetColor(standardColor);
                activeMenuButton++;
                menuButton[activeMenuButton].SetColor(selectedColor);
            }
        }
        public void MoveUp(){
            if(activeMenuButton != 0){
                menuButton[activeMenuButton].SetColor(standardColor);
                activeMenuButton--;
                menuButton[activeMenuButton].SetColor(selectedColor);
            }
        }

        public void SelectButton(GameEvent gameEvent){
            if(gameEvent.ObjectArg1 != MainMenu.GetInstance()){
                Breakout.Timer.ResumeTimer();
            }
            BreakoutBus.GetBus().RegisterEvent(gameEvent);
        }

    }


}