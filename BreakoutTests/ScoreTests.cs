using NUnit.Framework;
using DIKUArcade.Events;
using Breakout;
using System.Collections.Generic;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using System.IO;
using Breakout.Blocks;

namespace BreakoutTests;
    public class ScoreTests{
        private Block defaultBlock;
        private HardenedBlock hardenedBlock;
        private TeleportBlock teleportBlock;
        private PowerUpBlock powerUpBlock;


        [SetUp]
        public void SetupScoreTest(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            defaultBlock = new Block(
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")));
            hardenedBlock = new HardenedBlock(
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")),
                new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png")));
            teleportBlock = new TeleportBlock(
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")));
            powerUpBlock = new PowerUpBlock(
                new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")));
        }
        [Test]
        public void TestInitialScore(){
            Score.ResetPoints();
            Assert.AreEqual(0, Score.GetPoints());
        }
        [Test]
        public void TestAddPointsDefault(){
            Score.ResetPoints();
            Score.AddPoints(defaultBlock.GetValue());
            Assert.AreEqual(1, Score.GetPoints());
        }
        [Test]
        public void TestAddPointsHardened(){
            Score.ResetPoints();
            Score.AddPoints(hardenedBlock.GetValue());
            Assert.AreEqual(2, Score.GetPoints());
        }
        [Test]
        public void TestAddPointsTeleport(){
            Score.ResetPoints();
            Score.AddPoints(teleportBlock.GetValue());
            Assert.AreEqual(3, Score.GetPoints());
        }

        [Test]
        public void TestAddPointsPowerUpBlock(){
            Score.ResetPoints();
            Score.AddPoints(powerUpBlock.GetValue());
            Assert.AreEqual(1, Score.GetPoints());
        }

        [Test]
        public void TestResetScore(){
            Score.AddPoints(powerUpBlock.GetValue());
            Score.AddPoints(powerUpBlock.GetValue());
            Assert.AreNotEqual(0, Score.GetPoints());
            Score.ResetPoints();
            Assert.AreEqual(0, Score.GetPoints());
        }

        [Test]
        public void TestGameLostScore(){
            Vec2F pos = Score.GetDisplay().GetShape().Position;
            Score.GameOverScore(false);
            Assert.AreNotEqual(Score.GetDisplay().GetShape().Position, pos);
        }

        [Test]
        public void TestGameWonScore(){
            Vec2F pos = Score.GetDisplay().GetShape().Position;
            Score.GameOverScore(false);
            Assert.AreNotEqual(Score.GetDisplay().GetShape().Position, pos);
        }
    }
