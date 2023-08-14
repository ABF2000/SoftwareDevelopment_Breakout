using NUnit.Framework;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using System.IO;
using Breakout;
using Breakout.Blocks;
using System.Collections.Generic;
using Breakout.LevelLoading;
using Breakout.BreakoutStates;
using DIKUArcade.Events;
using Breakout.PowerUps;
 
// TODO: Add PowerUpBlock
namespace BreakoutTests;
    public class BlockTests{
        private Block defaultBlock;
        private HardenedBlock hardenedBlock;
        private UnbreakableBlock unbreakableBlock;
        private TeleportBlock teleportBlock;
        private PowerUpBlock powerUpBlock;

        private List<FormationData> map;
        private EntityContainer<Block> blocks;

        // private GameEventBus eventBus;


        [SetUp]
        public void Setup(){
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            defaultBlock = new Block(
                new DynamicShape(BlockHandler.ToShapePos((1,1)), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png"))
            );
            hardenedBlock = new HardenedBlock(
                new DynamicShape(BlockHandler.ToShapePos((1,2)), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png")),
                new Image(Path.Combine("Assets", "Images", "blue-block-damaged.png"))
            );
            unbreakableBlock = new UnbreakableBlock(
                new DynamicShape(BlockHandler.ToShapePos((1,3)), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png"))
            );
            teleportBlock = new TeleportBlock(
                new DynamicShape(BlockHandler.ToShapePos((4,4)), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png"))
            );
            powerUpBlock = new PowerUpBlock(
                new DynamicShape(BlockHandler.ToShapePos((5,5)), new Vec2F(0.083f, 0.04f)),
                new Image(Path.Combine("Assets", "Images", "blue-block.png"))
            );
            
            map = new List<FormationData>();

            map.Add(new FormationData{
                Image = "blue-block.png",
                GridPos = (1,1)
            });
            map.Add(new FormationData{
                Attribute = "Hardened",
                Image = "blue-block.png",
                GridPos = (1,2)
            });
            map.Add(new FormationData{
                Attribute = "Unbreakable",
                Image = "blue-block.png",
                GridPos = (1,3)
            });
            map.Add(new FormationData{
                Attribute = "Teleport",
                Image = "blue-block.png",
                GridPos = (4,4)
            });
            map.Add(new FormationData{
                Attribute = "PowerUp",
                Image = "blue-block.png",
                GridPos = (5,5)
            });
            blocks = new EntityContainer<Block>();
            BlockHandler.CreateBlockFormation(map, blocks);
            // BlockHandler.InitializeAvailablePositions(map);

            // eventBus = new GameEventBus();
            // eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.StatusEvent });
            // eventBus.Subscribe(GameEventType.StatusEvent, GameRunning.GetInstance());

        }

        [Test]
        public void TestValue()
        {
            Assert.AreEqual(defaultBlock.GetValue(), 1);
            Assert.AreEqual(hardenedBlock.GetValue(), 2);
            Assert.AreEqual(unbreakableBlock.GetValue(), 1);
            Assert.AreEqual(teleportBlock.GetValue(), 3);
            Assert.AreEqual(powerUpBlock.GetValue(), 1);
        }

        [Test]
        public void TestHealth()
        {
            Assert.AreEqual(defaultBlock.GetHealth(), 1);
            Assert.AreEqual(hardenedBlock.GetHealth(), 2);
            Assert.AreEqual(unbreakableBlock.GetHealth(), 1);
            Assert.AreEqual(teleportBlock.GetHealth(), 3);
            Assert.AreEqual(powerUpBlock.GetHealth(), 1);
        }

        [Test]
        public void TestDamageDefault()
        {
            var temp = defaultBlock.GetHealth();
            defaultBlock.TakeDamage(1);
            Assert.AreEqual(defaultBlock.GetHealth(), temp-1);
        }

        [Test]
        public void TestDamageHardened()
        {
            var temp = hardenedBlock.GetHealth();
            hardenedBlock.TakeDamage(1);
            Assert.AreEqual(hardenedBlock.GetHealth(), temp-1);
        }

        [Test]
        public void TestDamageUnbreakable()
        {
            var temp = unbreakableBlock.GetHealth();
            unbreakableBlock.TakeDamage(1);
            Assert.AreEqual(unbreakableBlock.GetHealth(), temp);
        }


        [Test]
        public void TestHardenedImage()
        {
            var temp = hardenedBlock.Image;
            hardenedBlock.TakeDamage(15);
            Assert.AreNotEqual(hardenedBlock.Image, temp);
        }

        [Test]
        public void TestDestroyed()
        {
            Assert.AreEqual(false, defaultBlock.IsDeleted());
            defaultBlock.TakeDamage(100);
            Assert.AreEqual(true, defaultBlock.IsDeleted());
        }

        [Test]
        public void TestDestroyedUnbreakable()
        {
            Assert.AreEqual(false, unbreakableBlock.IsDeleted());
            defaultBlock.TakeDamage(200);
            Assert.AreEqual(false, unbreakableBlock.IsDeleted());
        }


        // TODO: Fix teleport test
        [Test]
        public void TestTeleportedBlock()
        {
            (int,int) originalPosition = BlockHandler.ToGridPos(teleportBlock.Shape.Position);
            teleportBlock.TakeDamage(0);
            (int,int) newPosition = BlockHandler.ToGridPos(teleportBlock.Shape.Position);
            Assert.AreNotEqual(originalPosition, newPosition);
            // Assert.AreEqual(BlockHandler.availablePositions.Contains(newPosition), false);
        }


        [Test]
        public void TestPowerUpBlockType()
        {
            PowerUpType powerUpType = powerUpBlock.powerUpType;
            Assert.AreNotEqual(null, powerUpType);
        }

        // [Test]
        // public void TestAvailablePositions()
        // {
        //     map = new List<FormationData>();

        //     map.Add(new FormationData{
        //         GridPos = (1,1)
        //     });
        //     map.Add(new FormationData{
        //         GridPos = (1,2)
        //     });
        //     map.Add(new FormationData{
        //         GridPos = (1,3)
        //     });
        //     map.Add(new FormationData{
        //         GridPos = (4,4)
        //     });
        //     map.Add(new FormationData{
        //         GridPos = (5,5)
        //     });

        //     BlockHandler.initializeAvailablePositions(map);

        //     (int,int) originalPosition = BlockHandler.ToGridPos(teleportBlock1.Shape.Position);

        //     // System.Console.WriteLine("test: {0}", originalPosition);
        //     // foreach(var tal in BlockHandler.availablePositions){
        //     //     System.Console.WriteLine(tal);
        //     // }

        //     Assert.AreEqual(false, BlockHandler.availablePositions.Contains(originalPosition));
            
        //     eventBus.RegisterEvent(new GameEvent{
        //         EventType = GameEventType.StatusEvent,
        //         Message = "BLOCK_MOVED",
        //         From = teleportBlock1,
        //         ObjectArg1 = originalPosition
        //     });

        //     teleportBlock.TakeDamage(0);
        //     (int,int) newPosition = BlockHandler.ToGridPos(teleportBlock1.Shape.Position);
            


        //     eventBus.ProcessEventsSequentially();



        //     Assert.AreEqual(true, BlockHandler.availablePositions.Contains(originalPosition));
        //     Assert.AreEqual(false, BlockHandler.availablePositions.Contains(newPosition));
        // }




    }