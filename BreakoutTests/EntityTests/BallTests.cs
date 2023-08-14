using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using Breakout;
using Breakout.Balls;
using Breakout.LevelLoading;
using Breakout.Blocks;


namespace BreakoutTests;
    public class BallTests
    {

        private Player player;
        private Ball ball;
        private LevelLoader levelLoader;
        private string levelPath;
        private List<FormationData> map;
        private EntityContainer<Block> blockFormation;
        private string level;
        private string dir;

        [SetUp]

        public void InitiateBallTestEnvironment(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();

            levelLoader = new LevelLoader();

            blockFormation = new EntityContainer<Block>(200);
        }


        [Test]
        public void RightWindowEdgeBounce(){

            ball = new Ball(
                new DynamicShape( new Vec2F(0.96f, 0.5f), new Vec2F(0.03f, 0.03f), new Vec2F(0.05f, 0.0f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            Vec2F priorDirection = ball.shape.Direction;

            ball.Move();

            ball.EdgeBounce(ball.EdgeBounceDir());

            ball.Move();

            Assert.AreEqual(priorDirection.X *= -1.0f, ball.shape.Direction.X);
        }

        [Test]
        public void LeftWindowEdgeBounce(){

            ball = new Ball(
                new DynamicShape( new Vec2F(0.005f, 0.5f), new Vec2F(0.03f, 0.03f), new Vec2F(-0.05f, 0.0f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            Vec2F priorDirection = ball.shape.Direction;

            ball.Move();

            ball.EdgeBounce(ball.EdgeBounceDir());

            ball.Move();

            Assert.AreEqual(priorDirection.X *= -1.0f, ball.shape.Direction.X);
        }

        [Test]
        public void UpperWindowEdgeBounce(){

            ball = new Ball(
                new DynamicShape( new Vec2F(0.5f, 0.96f), new Vec2F(0.03f, 0.03f), new Vec2F(0.0f, 0.01f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            Vec2F priorDirection = ball.shape.Direction;

            ball.Move();

            ball.EdgeBounce(ball.EdgeBounceDir());

            Assert.AreEqual(priorDirection.X *= -1.0f, ball.shape.Direction.X);
        }

        [Test]
        public void BlockCollisionDirUp(){
            
            ball = new Ball(
                new DynamicShape(new Vec2F(0.5f, 0.49f), new Vec2F(0.03f, 0.03f), new Vec2F(0.0f, 0.01f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            level = "testlevel1.txt";

            dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            levelPath = dir[..(dir.Length-16)] + Path.Combine("Assets", "Levels", level);

            levelLoader.ReadFile(levelPath);

            map = levelLoader.GetMap();

            blockFormation = new EntityContainer<Block>(200);

            BlockHandler.CreateBlockFormation(map, blockFormation);

            Vec2F priorDirection = ball.shape.Direction;

            ball.Move();

            Assert.AreEqual(priorDirection.Y *= -1.0f, ball.shape.Direction.Y);
        }

        [Test]
        public void BlockCollisionDirDown(){

            ball = new Ball(
                new DynamicShape(new Vec2F(0.5f, 0.6f), new Vec2F(0.03f, 0.03f), new Vec2F(0.0f, -0.01f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            level = "testlevel1.txt";

            dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            levelPath = dir[..(dir.Length-16)] + Path.Combine("Assets", "Levels", level);

            levelLoader.ReadFile(levelPath);

            map = levelLoader.GetMap();

            blockFormation = new EntityContainer<Block>(200);

            BlockHandler.CreateBlockFormation(map, blockFormation);

            Vec2F priorDirection = ball.shape.Direction;

            ball.Move();

            Assert.AreEqual(priorDirection.Y *= -1.0f, ball.shape.Direction.Y);
        }

        [Test]
        public void BlockCollisionDirRight(){

            ball = new Ball(
                new DynamicShape( new Vec2F(0.6f, 0.5f), new Vec2F(0.03f, 0.03f), new Vec2F(-0.01f, 0.0f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            level = "testlevel2.txt";

            dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            levelPath = dir[..(dir.Length-16)] + Path.Combine("Assets", "Levels", level);

            levelLoader.ReadFile(levelPath);

            map = levelLoader.GetMap();

            blockFormation = new EntityContainer<Block>(200);

            BlockHandler.CreateBlockFormation(map, blockFormation);

            Vec2F priorDirection = ball.shape.Direction;

            ball.Move();

            Assert.AreEqual(priorDirection.X *= -1.0f, ball.shape.Direction.X);

        }

        [Test]
        public void BlockCollisionDirLeft(){

            ball = new Ball(
                new DynamicShape(new Vec2F(0.4f, 0.5f), new Vec2F(0.03f, 0.03f), new Vec2F(0.01f, 0.0f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            level = "testlevel2.txt";

            dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            levelPath = dir[..(dir.Length-16)] + Path.Combine("Assets", "Levels", level);

            levelLoader.ReadFile(levelPath);

            map = levelLoader.GetMap();

            blockFormation = new EntityContainer<Block>(200);

            BlockHandler.CreateBlockFormation(map, blockFormation);

            Vec2F priorDirection = ball.shape.Direction;

            ball.Move();

            Assert.AreEqual(priorDirection.X *= -1.0f, ball.shape.Direction.X);
        }

        [Test]
        public void PlayerCollisionLeftSide(){ 

            player = new Player(                                    
                new DynamicShape(new Vec2F(0.42f, 0.04f), new Vec2F(0.16f, 0.02f)), 
                new Image(Path.Combine("Assets", "Images", "player.png")));

            float leftPlayerPositionX = player.Shape.Position.X + (player.Shape.Extent.X / 6);

            ball = new Ball(
                new DynamicShape(new Vec2F(leftPlayerPositionX, 0.07f), new Vec2F(0.03f, 0.03f), new Vec2F(0.0f, -0.01f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            Vec2F priorDirection = ball.shape.Direction;

            float rndXDir = -ball.GenerateXDir();

            ball.Move();

            ball.PlayerBounce(player, rndXDir);

            ball.Move();

            Assert.AreEqual(priorDirection.Y *= -1.0f, ball.shape.Direction.Y);

            Assert.AreEqual(priorDirection.X += rndXDir, ball.shape.Direction.X);

        }

        [Test]
        public void PlayerCollisionRightSide(){ 

            player = new Player(                                    
                new DynamicShape(new Vec2F(0.42f, 0.04f), new Vec2F(0.16f, 0.02f)), 
                new Image(Path.Combine("Assets", "Images", "player.png")));

            float rightPlayerPositionX = player.Shape.Position.X + ((player.Shape.Extent.X / 1) * 5);

            ball = new Ball(
                new DynamicShape(new Vec2F(rightPlayerPositionX, 0.07f), new Vec2F(0.03f, 0.03f), new Vec2F(0.0f, -0.01f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            Vec2F priorDirection = ball.shape.Direction;

            float rndXDir = ball.GenerateXDir();

            ball.Move();

            ball.PlayerBounce(player, rndXDir);

            ball.Move();

            Assert.AreEqual(priorDirection.Y *= -1.0f, ball.shape.Direction.Y);

            Assert.AreEqual(priorDirection.X += rndXDir, ball.shape.Direction.X);

        }

        [Test]
        public void PlayerCollisionMiddle(){ 

            player = new Player(                                    
                new DynamicShape(new Vec2F(0.42f, 0.04f), new Vec2F(0.16f, 0.02f)), 
                new Image(Path.Combine("Assets", "Images", "player.png")));

            float middlePlayerPositionX = player.Shape.Position.X + (player.Shape.Extent.X / 2);

            ball = new Ball(
                new DynamicShape(new Vec2F(middlePlayerPositionX, 0.07f), new Vec2F(0.03f, 0.03f), new Vec2F(0.0f, -0.01f)),
                new Image(Path.Combine("Assets", "Images", "ball.png")));

            Vec2F priorDirection = ball.shape.Direction;

            float rndXDir = ball.GenerateXDir();

            ball.Move();

            ball.PlayerBounce(player, rndXDir);

            ball.Move();

            Assert.AreEqual(priorDirection.Y *= -1.0f, ball.shape.Direction.Y);

            Assert.AreEqual(priorDirection.X += rndXDir, ball.shape.Direction.X);
        }
    }
