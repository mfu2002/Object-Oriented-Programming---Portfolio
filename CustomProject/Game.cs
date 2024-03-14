using SplashKitSDK;
using System.Collections.Generic;
using System.Numerics;


namespace CustomProject
{
    internal class Game
    {


        public static readonly byte TILE_WIDTH = 50;


        private int _money = 100;
        private readonly List<DrawInstructions> _drawInstructions = new List<DrawInstructions>();
        private long _lastUpdateTime = DateTime.Now.Ticks;

        private EnemyProcessor _enemyGenerator;

        public EnemyProcessor EnemyGenerator
        {
            get { return _enemyGenerator; }
            set { _enemyGenerator = value; }
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



        public Game()
        {
            int[,] mapSchema = {
                    { 1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1},
                    {1, 1, 0, 0, 0, 0,0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 0, 1, 1, 1,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    {1, 1, 0, 1, 1, 1,0, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    {1, 1, 0, 1, 1, 1,0, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    {1, 1, 0, 1, 1, 1,0, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    {1, 1, 0, 0, 0, 0,0, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    {1, 1, 1, 1, 1, 1,1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 1},
                    {1, 1, 1, 1, 1, 1,1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                    {1, 1, 1, 1, 1, 1,1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1,1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1},


            };

            Grid = new Map(mapSchema);


            EnemyGenerator = new EnemyProcessor(mapSchema);

        }

        public void HandleKeyInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Grid.SelectTileAt(new Vector2(SplashKit.MouseX(), SplashKit.MouseY()));
            }

            if (Grid.SelectedTile is ConstructableTile constructableTile)
            {

                if (constructableTile.Tower == null)
                {
                    if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                    {
                        constructableTile.Tower = new DefenceTower(constructableTile.Location + new Vector2(TILE_WIDTH, TILE_WIDTH) / 2, EnemyGenerator.Enemies);

                    }
                }
                else
                {
                    if (SplashKit.KeyTyped(KeyCode.AKey))
                    {

                        // do something
                    }else if (SplashKit.KeyTyped(KeyCode.SKey))
                    {
                        // do something else
                    }
                    else if (SplashKit.KeyTyped(KeyCode.DKey))
                    {
                        // do something else
                    }
                }
            }
        }
      

        public void Start()
        {
            Window window = new Window("Game", 850, 750);

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
            float deltaTime = (DateTime.Now.Ticks - _lastUpdateTime) / 10000000f;

            Grid.Update(deltaTime);
            EnemyGenerator.Update(deltaTime);
            


            _lastUpdateTime = DateTime.Now.Ticks;

        }


        public void Render()
        {


            Grid.GetDrawInstructions(_drawInstructions);
            EnemyGenerator.GetDrawInstructions(_drawInstructions);


            _drawInstructions.Sort((x, y) => x.Z.CompareTo(y.Z));

            foreach (DrawInstructions instructions in _drawInstructions)
            {
                instructions.Draw();
            }
            _drawInstructions.Clear();

        }


    }
}
