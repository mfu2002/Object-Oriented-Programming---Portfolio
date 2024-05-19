using System.Numerics;

namespace CustomProject
{
    public class Map : IDraw
    {
        /// <summary>
        /// A 2D list of tiles on screen.
        /// </summary>
        public Tile[,] Grid { get; }

        /// <summary>
        /// The tile that is currently selected.
        /// </summary>
        public Tile? SelectedTile
        {
            get
            {
                foreach (Tile tile in Grid)
                {
                    if (tile.Selected) return tile;
                }
                return null;
            }
        }

        /// <summary>
        /// Creates the map from the schema.
        /// </summary>
        /// <param name="mapSchema">2D int array of the schema</param>
        public Map(int[,] mapSchema)
        {
            Grid = new Tile[mapSchema.GetLength(0), mapSchema.GetLength(1)];
            ConvertSchemaToTiles(mapSchema);
        }


        /// <summary>
        /// Selects the tile(s) that the pt is over. 
        /// </summary>
        /// <param name="pt"></param>
        public void SelectTileAt(Vector2 pt)
        {
            foreach (Tile tile in Grid)
            {
                tile.Selected = tile.IsAt(pt) && tile.Selectable;
            }
        }

        /// <summary>
        /// Uses the schema to create the tiles for this object. 
        /// </summary>
        /// <param name="mapSchema">2D int array of the schema</param>
        /// <exception cref="Exception">Throws an exception if the schema contains invalid parameters. </exception>
        private void ConvertSchemaToTiles(int[,] mapSchema)
        {

            for (int row = 0; row < mapSchema.GetLength(0); row++)
            {
                for (int col = 0; col < mapSchema.GetLength(1); col++)
                {
                    Tile tile;
                    Vector2 tileLoc = new Vector2(col, row) * Game.TILE_WIDTH;
                    if (mapSchema[row, col] == 0)
                    {
                        tile = new PathTile(tileLoc);

                    }
                    else if (mapSchema[row, col] == 1)
                    {
                        tile = new ConstructableTile(tileLoc);
                    }
                    else if (mapSchema[row, col] == -1)
                    {
                        tile = new BlockedTile(tileLoc);
                    }
                    else
                    {
                        throw new Exception("Invalid Tile");
                    }
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
