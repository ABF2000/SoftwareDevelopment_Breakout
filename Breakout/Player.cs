using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout{

    /// <summary>
    /// Class for handling the concept of a player
    /// </summary>
    public class Player : Entity, IGameEventProcessor {        
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        const float movementSpeed = 0.01f;
        

        /// <summary> Finds the visually central position of the player with respect for the 
        /// size of another entity </summary>
        /// <param name="extent"> The extent of the entity to be compensated for </param>
        /// <returns> A Vec2F representing the "central" position of the player </returns>
        public Vec2F CenterPos(Vec2F extent){
            return new Vec2F(
                Shape.Position.X + Shape.Extent.X/2 - extent.X/2,
                Shape.Position.Y + Shape.Extent.Y/2 - extent.Y/2
            );
        }

        public Player(DynamicShape shape, IBaseImage image) : base(shape, image){
        }
        public void Render() {
            RenderEntity();
        }
        /// <summary>
        /// Moves the player
        /// </summary>
        public void Move(){
            if(Shape.AsDynamicShape().Direction.X > 0.0f && Shape.Position.X <= 0.84f){
                Shape.Move();
            }
            if(Shape.AsDynamicShape().Direction.X < 0.0f && Shape.Position.X >= 0.0f){
                Shape.Move();
            }
        }
        private void UpdateDirection(){
            Shape.AsDynamicShape().Direction.X = moveRight + moveLeft;
        }
        private void SetMoveLeft(bool val){
            if (val) moveLeft -= movementSpeed; else moveLeft = 0.0f;
            UpdateDirection();
        }
        private void SetMoveRight(bool val){
            if (val) moveRight += movementSpeed; else moveRight = 0.0f;
            UpdateDirection();
        }

        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.EventType == GameEventType.PlayerEvent){
                switch (gameEvent.Message){
                    case("LEFT_KEYPRESS"):
                        SetMoveLeft(true);
                        break;
                    case("RIGHT_KEYPRESS"):
                        SetMoveRight(true);
                        break;
                    case("LEFT_KEYRELEASE"):
                        SetMoveLeft(false);
                        break;
                    case("RIGHT_KEYRELEASE"):
                        SetMoveRight(false);
                        break;
                    case("A_KEYPRESS"):
                        SetMoveLeft(true);
                        break;
                    case("D_KEYPRESS"):
                        SetMoveRight(true);
                        break;
                    case("A_KEYRELEASE"):
                        SetMoveLeft(false);
                        break;
                    case("D_KEYRELEASE"):
                        SetMoveRight(false);
                        break;
                    default:
                        break;
                    }
                }
            }
    }
}