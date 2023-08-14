using NUnit.Framework;
using DIKUArcade.Events;
using Breakout;
using System.Collections.Generic;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.IO;

namespace BreakoutTests;
    public class PlayerTests
    {
        private Player player;
        private GameEventBus eventBus;
        
        [SetUp]
        
        public void InitiatePlayer(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            player = new Player(                                    
                new DynamicShape(new Vec2F(0.42f, 0.04f), new Vec2F(0.16f, 0.02f)), 
                new Image(Path.Combine("Assets", "Images", "player.png")));
            
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {GameEventType.PlayerEvent});
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }

        [Test]
        public void PlayerMoveLeft(){
            
            float posX = player.Shape.Position.X;

            eventBus.RegisterEvent(new GameEvent{
                EventType = GameEventType.PlayerEvent,
                Message = "LEFT_KEYPRESS"});
            

            eventBus.ProcessEventsSequentially();
            player.Move();

            Assert.AreEqual(posX - 0.01f, player.Shape.Position.X);
        }

        [Test]
        public void PlayerMoveRight(){

            float posX = player.Shape.Position.X;


            eventBus.RegisterEvent(new GameEvent{
                EventType = GameEventType.PlayerEvent,
                Message = "RIGHT_KEYPRESS"});
            

            eventBus.ProcessEventsSequentially();
            player.Move();

            Assert.AreEqual(posX + 0.01f, player.Shape.Position.X);
        }

        [Test]
        public void PlayerLeftEdge(){

            eventBus.RegisterEvent(new GameEvent{
                EventType = GameEventType.PlayerEvent,
                Message = "LEFT_KEYPRESS"});
            

            eventBus.ProcessEventsSequentially();

            int N = 0;

            while(N < 46){
                player.Move();
                N++;
            }

            float posX = player.Shape.Position.X;
            player.Move();

            Assert.AreEqual(player.Shape.Position.X, posX);
        }

        [Test]
        public void PlayerRightEdge(){

            eventBus.RegisterEvent(new GameEvent{
                EventType = GameEventType.PlayerEvent,
                Message = "RIGHT_KEYPRESS"});
            

            eventBus.ProcessEventsSequentially();

            int N = 0;

            while(N < 47){
                player.Move();
                N++;
            }

            float posX = player.Shape.Position.X;
            player.Move();

            Assert.AreEqual(player.Shape.Position.X, posX);
        }
        

        // [Test]
        // public void TestInitialLives(){
        //     Assert.AreEqual(3, player.getLives());
        // }

        // [Test]
        // public void TestRemoveLife(){
        //     player.ChangeLives(-1);
        //     Assert.AreEqual(2, player.getLives());
        // }

        // [Test]
        // public void TestAddLife(){
        //     player.ChangeLives(1);
        //     Assert.AreEqual(4, player.getLives());
        // }

        // [Test]
        // public void TestMoreLivesThanMaximum(){
        //     player.ChangeLives(5);
        //     Assert.AreEqual(5, player.getLives());
        // }
    }