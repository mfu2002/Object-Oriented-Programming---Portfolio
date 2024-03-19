using SplashKitSDK;
using System.Collections.Generic;
using System.Numerics;


namespace CustomProject
{
    internal class Game
    {


        public static readonly byte TILE_WIDTH = 50;
        private Sprite _heartSprite = new Sprite("heart10x10.png");


        private int _money = 100;
        private readonly List<DrawInstructions> _drawInstructions = new List<DrawInstructions>();
        private long _lastUpdateTime = DateTime.Now.Ticks;

        private EnemyCommander _enemyGenerator;

        public EnemyCommander EnemyGenerator
        {
            get { return _enemyGenerator; }
            set { _enemyGenerator = value; }
        }


        private int _lives = 3;

        public int Lives
        {
            get { return _lives; }
            set
            {
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


        private Map _map;

        public Map Grid
        {
            get { return _map; }
            set { _map = value; }
        }


        public int Money
        {
            get { return _money; }
            set { _money = value; }
        }



        public Game(int[,] mapSchema)
        {

            _map = new Map(mapSchema);
            _enemyGenerator = new EnemyCommander(mapSchema);
            AttachEnemiesToTower();

        }
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

        public void HandleKeyInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Grid.SelectTileAt(new Vector2(SplashKit.MouseX(), SplashKit.MouseY()));
            }

            if (Grid.SelectedTile is ConstructableTile constructableTile)
            {
                Money -= constructableTile.Tower.HandleUserInput(Money);
            }
        }


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


        public void Update()
        {
            if (Lives == 0) { _lastUpdateTime = DateTime.Now.Ticks; }
            float deltaTime = (DateTime.Now.Ticks - _lastUpdateTime) / 10000000f;
            Grid.Update(deltaTime);
            EnemyGenerator.Update(deltaTime);
            Money += EnemyGenerator.TakeReward();
            Lives -= EnemyGenerator.CheckVictoriousEnemies();
            _lastUpdateTime = DateTime.Now.Ticks;

        }

        private void GetHUDInstructions(List<DrawInstructions> instructions)
        {

            instructions.Add(new DrawInstructions(() => {
                SplashKit.DrawText($"${Money}", Color.Black, 10, 10);
                int horizontalOffset = 100;
                for (int i = 0; i< Lives; i++)
                {
                    SplashKit.DrawSprite(_heartSprite, SplashKit.ScreenWidth() - horizontalOffset, 10);
                    horizontalOffset -= 25;
                }
            }
            , 10));
        }

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
                SplashKit.DrawText("GAME OVER", Color.Black, SplashKit.ScreenWidth()/2, SplashKit.ScreenHeight()/2);
            }
        }


    }
}
