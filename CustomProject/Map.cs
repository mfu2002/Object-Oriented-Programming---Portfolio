using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Map : IDraw
    {

        private readonly Tile[,] _grid;

        public Tile[,] Grid { get { return _grid; } }


        public Tile? SelectedTile
        {
            get
            {
                foreach (Tile tile in _grid)
                {
                    if (tile.Selected) return tile;
                }
                return null;
            }
        }

        public Map(int[,] mapSchema)
        {
            _grid = new Tile[mapSchema.GetLength(0), mapSchema.GetLength(1)];
            ConvertSchemaToTiles(mapSchema);
        }



        public void SelectTileAt(Vector2 pt)
        {
            foreach (Tile tile in Grid)
            {
                tile.Selected = tile.IsAt(pt) && tile.Selectable;
            }
        }


        private void ConvertSchemaToTiles(int[,] mapSchema)
        {

            for (int row = 0; row < mapSchema.GetLength(0); row++)
            {
                for (int col = 0; col < mapSchema.GetLength(1); col++)
                {
                    Tile tile;
                    if (mapSchema[row, col] == 0)
                    {
                        tile = new PathTile();

                    }
                    else if (mapSchema[row, col] == 1)
                    {
                        tile = new ConstructableTile();
                    }
                    else if (mapSchema[row, col] == -1)
                    {
                        tile = new BlockedTile();
                    }
                    else
                    {
                        throw new Exception("Invalid Tile");
                    }
                    tile.Location = new Vector2(col, row) * Game.TILE_WIDTH;
                    Grid[row, col] = tile;
                }
            }

        }

        public void Update(float deltaTime)
        {
            foreach (Tile tile in Grid)
            {
                tile.Update(deltaTime);
            }
            HandleKeyInput();
        }

        public void HandleKeyInput()
        {
           
        }



        public void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            foreach (Tile tile in Grid)
            {
                tile.GetDrawInstructions(instructions);
            }
        }
    }
}
