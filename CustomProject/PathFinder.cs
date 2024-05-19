using System.Numerics;

namespace CustomProject
{
    public class PathFinder
    {

        /// <summary>
        /// Precomputed results indicating which direction the enemy should move next.
        /// </summary>
        private readonly Vector2[,] _directions;

  
        /// <summary>
        /// <see cref="EntryPoint"/>
        /// </summary>
        private (int, int)? _entryPoint = null;

        /// <summary>
        /// <see cref="ExitPoint"/>
        /// </summary>

        private (int, int)? _exitPoint = null;

        /// <summary>
        /// Map schema. 
        /// </summary>
        private readonly int[,] _map;
        /// <summary>
        /// Entry point of the map.
        /// </summary>
        public (int, int) EntryPoint
        {
            get
            {   
                // Lazy loading of the entry point. 
                if (_entryPoint == null)
                {
                    for (int i = 0; i < _map.GetLength(1); i++)
                    {
                        if (_map[0, i] == 0)
                        {
                            _entryPoint = (i, 0);
                            break;
                        }
                    }
                }
                if (_entryPoint == null)
                {
                    throw new Exception("Entry point not found.");
                }
                return ((int, int))_entryPoint;
            }
        }

        /// <summary>
        /// Last point on the path. 
        /// </summary>
        public (int, int) ExitPoint
        {
            get
            {
                // Lazy loading of the exit point. 
                if (_exitPoint == null)
                {
                    int lastRow = _map.GetLength(0) - 1;
                    for (int i = 0; i < _map.GetLength(1); i++)
                    {
                        if (_map[lastRow, i] == 0)
                        {
                            _exitPoint = (i, lastRow);
                            break;
                        }
                    }
                }
                if (_exitPoint == null)
                {
                    throw new Exception("Exit point not found.");
                }
                return ((int, int))_exitPoint;
            }
        }

        /// <summary>
        /// Checks whether there is a tile in that direction that is a path and has not already been visited. 
        /// </summary>
        /// <param name="grid">The Map schema</param>
        /// <param name="visited">Boolean 2D array of the tiles already visited</param>
        /// <param name="row">target row</param>
        /// <param name="col">target column</param>
        /// <param name="total_rows">Total number of rows in grid</param>
        /// <param name="total_columns">Total Number of columns in grid</param>
        /// <returns></returns>
        private static bool IsValidMove(int[,] grid, bool[,] visited, int row, int col, int total_rows, int total_columns)
        {
            return row >= 0 && row < total_rows     // within x bounds
                && col >= 0 && col < total_columns  // within y bounds
                && grid[row, col] == 0         // is a path
                && !visited[row, col];          // which we haven't already visited.
        }

        public Vector2 GetDirection(int x, int y)
        {
            return _directions[y, x];
        }

        public PathFinder(int[,] grid)
        {
            _map = grid;
            _directions = new Vector2[_map.GetLength(0), _map.GetLength(1)];
            SearchPath();
        }

        /// <summary>
        /// Uses an breadth first search algorithm to find the path from the start of the map to the end. 
        /// </summary>
        private void SearchPath()
        {
            int rows = _map.GetLength(0);
            int columns = _map.GetLength(1);


            bool[,] visited = new bool[rows, columns];

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue(EntryPoint);
            visited[EntryPoint.Item2, EntryPoint.Item1] = true;



            Vector2[] directions = [
                new Vector2(-1,0),
                new Vector2(0,1),
                new Vector2(1,0),
                new Vector2(0,-1),
                ];
            while (queue.Count > 0)
            {
                var (col, row) = queue.Dequeue();



                foreach (Vector2 direction in directions)
                {
                    int newRow = (int)(row + direction.Y);
                    int newCol = (int)(col + direction.X);

                    if (IsValidMove(_map, visited, newRow, newCol, rows, columns))
                    {
                        visited[newRow, newCol] = true;
                        queue.Enqueue((newCol, newRow));
                        _directions[row, col] = direction;
                    }
                }

            }


        }
    }
}
