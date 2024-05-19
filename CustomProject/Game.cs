using SplashKitSDK;
using System.Numerics;


namespace CustomProject
{
    internal class Game
    {
        /// <summary>
        /// Size of each tile.
        /// </summary>
        public static readonly byte TILE_WIDTH = 50;

        /// <summary>
        /// readonly reference to the heart sprite.
        /// </summary>
        private readonly Sprite _heartSprite = new Sprite("heart10x10.png");

        /// <summary>
        /// Used for rendering.
        /// </summary>
        private readonly List<DrawInstructions> _drawInstructions = [];

        /// <summary>
        /// Keeps track of when the last game cycle was run. 
        /// </summary>
        private long _lastUpdateTime = DateTime.Now.Ticks;

        /// <summary>
        /// <see cref="Lives"/>
        /// </summary>
        private int _lives = 3;


        public EnemyCommander EnemyGenerator { get; set; }


        public Map Grid { get; private set; }

        /// <summary>
        /// Current money of the player. 
        /// </summary>
        public int Money { get; private set; } = 100;

        /// <summary>
        /// Player lives.
        /// </summary>
        public int Lives
        {
            get { return _lives; }
            set
            {
                // Ensure the lives does not go in negative. 
                if (value < 0)
                {
                    _lives = 0;
                }
                else
                {
                    _lives = value;
                }
            }
        }



        public Game(int[,] mapSchema)
        {

            Grid = new Map(mapSchema);
            EnemyGenerator = new EnemyCommander(mapSchema);
            AttachEnemiesToTower();

        }


        /// <summary>
        /// Initialises the game components and starts the game loop. 
        /// </summary>
        public void Start()
        {
            Window window = new Window("Game", TILE_WIDTH * Grid.Grid.GetLength(1), TILE_WIDTH * Grid.Grid.GetLength(0));

            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();
                HandleKeyInput();
                Update();
                Render();
                SplashKit.RefreshScreen();
            } while (!window.CloseRequested);
        }


        /// <summary>
        /// Creates a DefenceTower on each constructable tile and attaches the reference to the enemy list to it. 
        /// </summary>
        private void AttachEnemiesToTower()
        {
            foreach (var tile in Grid.Grid)
            {
                if (tile is ConstructableTile constructableTile)
                {
                    constructableTile.Tower = new DefenceTower(constructableTile.Location + new Vector2(TILE_WIDTH, TILE_WIDTH) / 2, EnemyGenerator.Enemies);
                }
            }
        }


        /// <summary>
        /// Handle user mouse and keyboard input. 
        /// </summary>
        public void HandleKeyInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Grid.SelectTileAt(new Vector2(SplashKit.MouseX(), SplashKit.MouseY()));
            }

            if (Grid.SelectedTile is ConstructableTile constructableTile)
            {
                Money -= constructableTile.Tower!.HandleUserInput(Money);
            }
        }




        public void Update()
        {
            if (Lives == 0) { _lastUpdateTime = DateTime.Now.Ticks; }
            float deltaTime = (DateTime.Now.Ticks - _lastUpdateTime) / 10000000f;
            Grid.Update(deltaTime);
            EnemyGenerator.Update(deltaTime);
            Money += EnemyGenerator.CheckDeadEnemies();
            Lives -= EnemyGenerator.CheckVictoriousEnemies();
            _lastUpdateTime = DateTime.Now.Ticks;

        }

        /// <summary>
        /// Gets instructions for the HUD components like the money, lives and stats. 
        /// </summary>
        /// <param name="instructions"></param>
        private void GetHUDInstructions(List<DrawInstructions> instructions)
        {

            instructions.Add(new DrawInstructions(() =>
            {
                SplashKit.DrawText($"${Money}", Color.Black, 10, 10);
                int horizontalOffset = 100;
                for (int i = 0; i < Lives; i++)
                {
                    SplashKit.DrawSprite(_heartSprite, SplashKit.ScreenWidth() - horizontalOffset, 10);
                    horizontalOffset -= 25;
                }
            }
            , 10));
        }

        /// <summary>
        /// Handles the rendering of the objects using the z-level
        /// </summary>
        public void Render()
        {

            GetHUDInstructions(_drawInstructions);

            Grid.GetDrawInstructions(_drawInstructions);
            EnemyGenerator.GetDrawInstructions(_drawInstructions);


            _drawInstructions.Sort((x, y) => x.Z.CompareTo(y.Z));

            foreach (DrawInstructions instructions in _drawInstructions)
            {
                instructions.Draw();
            }
            _drawInstructions.Clear();

            if (Lives == 0)
            {
                SplashKit.DrawText("GAME OVER", Color.Black, SplashKit.ScreenWidth() / 2, SplashKit.ScreenHeight() / 2);
            }
        }


    }
}
