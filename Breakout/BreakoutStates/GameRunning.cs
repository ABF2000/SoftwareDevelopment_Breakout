using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using Breakout.LevelLoading;
using DIKUArcade.Events;
using DIKUArcade.Math;
using DIKUArcade.Input;
using Breakout.Blocks;
using Breakout.Balls;
using DIKUArcade.State;
using Breakout.PowerUps;


namespace Breakout.BreakoutStates;
/// <summary>
/// Central class for handling the game when it is running
/// </summary>
public class GameRunning : IGameState, IGameEventProcessor {
    private Entity backGroundImage;
    public bool HardBall;
    public bool Invincible;
    public bool BigBall;
    private Player player;
    private Vec2F ballExtent = new Vec2F(0.03f, 0.03f);
    private EntityContainer<Ball> balls;
    private EntityContainer<PowerUp> powerUps;
    private string dir;
    private string levelPath;
    private LevelLoader loader;
    public LevelLoader GetLevelLoader(){return loader;}
    private List<FormationData> map;
    private List<FormationData> meta;
    private long levelTime;
    private Text levelName;
    private EntityContainer<Block> blockFormation;
    private int levelCount;
    private static GameRunning instance = null;
    public static GameRunning GetInstance () {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
        }
        return GameRunning.instance;
    }

    /// <summary> Initializes a fresh new game state </summary>
    private void InitializeGameState(){
        backGroundImage = new (
            new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        
        player = new Player(                                    
            new DynamicShape(new Vec2F(0.42f, 0.04f), new Vec2F(0.16f, 0.02f)), 
            new Image(Path.Combine("Assets", "Images", "player.png")));

        powerUps = new EntityContainer<PowerUp>();

        balls = new EntityContainer<Ball>();

        blockFormation = new EntityContainer<Block>();

        loader = new LevelLoader();
        map = new List<FormationData>();
        meta = new List<FormationData>();

        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);


        ResetState();
    }


    /// <summary> Initializes the next level </summary>
    /// <param name = "level"> The identifier for which level to pick </param>
    private void InitializeLevel(int level){

        dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        levelPath = dir[..(dir.Length-16)] + Path.Combine("Assets", "Levels", "level" +
                        level.ToString() + ".txt");

        loader.ReadFile(levelPath);


        map = loader.GetMap();
        meta = loader.GetMeta();
        System.Console.WriteLine(map.Count);
        levelTime = -1;
        levelName = new Text("", new Vec2F(0.0f, 0.0f), new Vec2F(0.0f, 0.0f));
        foreach(var m in meta){
            if(m.Attribute == "Time"){
                if(long.TryParse(m.Symbol, out long time)){
                    levelTime = time;
                }
            }
            if(m.Attribute == "Name"){
                levelName.SetText(m.Symbol);
                levelName.GetShape().Position = new Vec2F(0.8f, -0.255f);
                levelName.SetColor(new Vec3I(255, 255, 0));
                levelName.GetShape().Extent = new Vec2F(0.3f, 0.3f);
            }
        }
        Breakout.Timer.SetTimer(levelTime);
        BlockHandler.CreateBlockFormation(map, blockFormation);
    }

    /// <summary> Calls the InitializeLevel() method if the given level is valid, otherwise
    /// change the state to MainMenu </summary>
    private void AdvanceLevel(){
        levelCount++;

        dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        levelPath = dir[..(dir.Length-16)] + Path.Combine("Assets", "Levels", 
                                            "level" + levelCount.ToString() + ".txt");


        if (File.Exists(levelPath)){
            loader = new LevelLoader();
            blockFormation.ClearContainer();
            balls.ClearContainer();
            balls.AddEntity(new Ball(
                new DynamicShape(player.CenterPos(ballExtent), ballExtent),
                new Image(Path.Combine("Assets", "Images", "ball.png"))));
            powerUps.ClearContainer();
            InitializeLevel(levelCount);
        }
        else {
            BreakoutBus.GetBus().RegisterEvent(
                new GameEvent{
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    ObjectArg1 = GameWon.GetInstance()
                }
            );
        }

    }

    /// <summary> Method for the UpdateState() method to continuously check when to change
    /// level/gamestate </summary>   
    private void NextLevelChecker(){
        bool advance = true;
            blockFormation.Iterate(block =>{
                if(block is not UnbreakableBlock){
                    advance = false;
                }
            });
        if(advance){
            AdvanceLevel();
        }
    }
    /// <summary> Creates new game events depending on keys pressed, ie. left and right arrow
    /// keys, or A and D </summary>
    /// <param name = "action"> The act of pressing a key </param>
    /// <param name = "key"> The key that is pressed </param>
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
        if(action == KeyboardAction.KeyPress){
            switch (key){
                case(KeyboardKey.L):
                    blockFormation.Iterate(block => {
                        if(block is not UnbreakableBlock){
                            block.DeleteBlock();
                        }
                    });
                    break;
                case(KeyboardKey.Escape):
                    Breakout.Timer.PauseTimer();
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg2 = "RESET",
                        ObjectArg1 = GamePaused.GetInstance()});
                    break;
                case(KeyboardKey.Space):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.InputEvent,
                        Message = "SPACE_KEYPRESS"});
                    break;
                case(KeyboardKey.Left):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "LEFT_KEYPRESS"});
                    break;
                case(KeyboardKey.Right):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "RIGHT_KEYPRESS"});
                    break;
                case(KeyboardKey.A):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "A_KEYPRESS"});
                    break;
                case(KeyboardKey.D):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "D_KEYPRESS"});
                    break;
                default:
                    break;
            }
        }
        if(action == KeyboardAction.KeyRelease){
            switch(key){
                case(KeyboardKey.Left):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "LEFT_KEYRELEASE"});
                    break;
                case(KeyboardKey.Right):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "RIGHT_KEYRELEASE"});
                    break;
                case(KeyboardKey.A):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "A_KEYRELEASE"});
                    break;
                case(KeyboardKey.D):
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.PlayerEvent,
                        Message = "D_KEYRELEASE"});
                    break;                                                                                
                default:
                    break;              
            }
        }
    }
    
    /// <summary> Determines how the bal(s) should move based on its interactions with the
    /// player, walls or blocks. </summary>
    private void IterateBalls(){
        balls.Iterate(ball =>{
            if(ball.shape.Direction.Y == 0.0f){
                ball.shape.SetPosition(player.CenterPos(ballExtent));
            }
            ball.Move();
            if (ball.shape.Position.Y < 0.0f) {
                ball.DeleteEntity();
            }
            else if (ball.shape.Position.X < 0.0f || 
                        ball.shape.Position.X > 0.97f || 
                        ball.shape.Position.Y > 0.97f){
                string bouncedir = ball.EdgeBounceDir();
                ball.EdgeBounce(bouncedir);
            }
            else if((CollisionDetection.Aabb(ball.shape, player.Shape)).Collision){
                if (ball.IsLeftSideBounce(player) || ball.IsRightSideBounce(player)){
                    ball.PlayerBounce(player, ball.GenerateXDir());
                }
                else {
                    ball.PlayerBounce(player, ball.GenerateXDirMidCollision());
                }

            }
            else {
                IterateBlocks(ball);
            }

        });
        if(balls.CountEntities() == 0){
            HardBall = false;
            BigBall = false;
            BreakoutBus.GetBus().CancelTimedEvent(1u);
            BreakoutBus.GetBus().CancelTimedEvent(2u);
            if(!Invincible){
                Life.ChangeLives(-1);
            }
            balls.AddEntity(new Ball(
                new DynamicShape(player.CenterPos(ballExtent), ballExtent),
                new Image(Path.Combine("Assets", "Images", "ball.png"))));
        }
    }
    

    /// <summary> Iterates through the blocks to determine collisions and to add power-ups 
    /// </summary>
    /// <param name = "ball"> A ball type </param>
    private void IterateBlocks(Ball ball){
        blockFormation.Iterate(block => {
            if ((CollisionDetection.Aabb(ball.shape, block.Shape)).Collision){
                CollisionDirection bouncedir = 
                        CollisionDetection.Aabb(ball.shape, block.Shape).CollisionDir;
                ball.BlockBounce(bouncedir);
                if(HardBall){
                    block.DeleteBlock();
                }
                else{
                    block.TakeDamage(1);
                }

                if(block.IsDeleted()){
                    Score.AddPoints(block.GetValue());
                    if(block is PowerUpBlock){
                        PowerUpBlock powerUpBlock = (PowerUpBlock)block;
                        PowerUpHandler.CreatePowerUp(powerUps, powerUpBlock);
                    }
                }
            }
        });
    }


    /// <summary> Iterates through all power-ups to determine collision between it and the player
    /// and then activate the powerUp if there is a collision </summary>
    private void IteratePowerUp(){
        powerUps.Iterate( powerUp => {
            powerUp.Move();
            if(powerUp.Shape.Position.Y <= 0.0f){
                powerUp.DeleteEntity();
            }
            if(CollisionDetection.Aabb(powerUp.Shape.AsDynamicShape(), player.Shape).Collision){
                powerUp.DeleteEntity();
                powerUp.ActivatePowerUp();
            }
        });
    }

    /// <summary> shoots the balls out from the player </summary>
    private void LaunchBalls(){
        balls.Iterate(ball =>{
            if(ball.shape.Direction.Y == 0.0f){
                ball.shape.Direction.Y = 0.01f;
            }
        });
    }

    /// <summary> Returns the player to the gamelost screen if the player has 0 lives </summary>
    private void IsPlayerAlive(){
        if(!Life.IsAlive){
            Breakout.Timer.PauseTimer();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                ObjectArg1 = GameLost.GetInstance()});
        }
    }


    /// <summary> Returns the player to the gamelost screen if the timer reaches 0 </summary>
    private void IsTimerZero(){
        if (Breakout.Timer.IsZero){
            Breakout.Timer.PauseTimer();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                ObjectArg1 = GameLost.GetInstance()});
        }
    }
   
    /// <summary> Handles and processes the received event </summary>
    /// <param name = "gameEvent"> A specific gameevent of gameeventtype </param>
    public void ProcessEvent(GameEvent gameEvent){
        if(gameEvent.EventType == GameEventType.InputEvent){
            switch(gameEvent.Message){
                case("Split"):case("Hard_Ball"):case("Double_Size"):
                    IBallPowerUp powerUpBall = (IBallPowerUp)gameEvent.From;
                    if(gameEvent.StringArg1 == "ACTIVATED"){
                        powerUpBall.BallPowerUp(balls, true);
                    }
                    else{
                        powerUpBall.BallPowerUp(balls, false);
                    }
                    break;
                case("Extra_Life"):case("Invincible"):
                    IGamePowerUp powerUpGame = (IGamePowerUp)gameEvent.From;
                    if(gameEvent.StringArg1 == "ACTIVATED"){
                        powerUpGame.GamePowerUp(true);
                    }
                    else{
                        powerUpGame.GamePowerUp(false);
                    }
                    break;
                case("SPACE_KEYPRESS"):
                    LaunchBalls();
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary> Renders the player, formation of blocks, the ball, overview of points,
    ///, and the tim </summary>
    public void RenderState(){
        backGroundImage.RenderEntity();
        player.Render();
        foreach(Block obj in blockFormation){
            obj.RenderEntity();
        }
        balls.RenderEntities();
        Score.RenderPoints();
        Timer.RenderTimer();
        powerUps.RenderEntities();
        Life.RenderLives();
        levelName.RenderText();
        PowerUpHandler.RenderActivePowerUps(new List<bool>{HardBall, Invincible, BigBall});
    }
    
    /// <summary> Updates gamestate </summary>
    public void UpdateState(){
        Timer.UpdateTimer();
        IsPlayerAlive();
        IsTimerZero();
        player.Move();
        IterateBalls();
        IteratePowerUp();
        NextLevelChecker();
    }
    
    /// <summary> Resets gamestate </summary>
    public void ResetState(){
        balls.ClearContainer();
        blockFormation.ClearContainer();
        powerUps.ClearContainer();
        map.Clear();
        meta.Clear();
        Score.ResetPoints();
        Timer.RestartTimer();
        player.Shape.SetPosition(new Vec2F(0.42f, 0.04f));
        Life.ResetLives();
        balls.AddEntity(new Ball(
            new DynamicShape(player.CenterPos(ballExtent), ballExtent),
            new Image(Path.Combine("Assets", "Images", "ball.png"))));
        HardBall = false;
        Invincible = false;
        BigBall = false;
        levelCount = 1;
        InitializeLevel(levelCount);
    }
}