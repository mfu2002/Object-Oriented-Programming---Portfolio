using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class PathFinder
    {
        private Vector2[,] _directions;
        private (int, int)? _entryPoint = null;
        private int[,] _map;

        public (int, int) EntryPoint
        {
            get
            {
                if (_entryPoint == null)
                {
                    for (int i = 0; i < _map.GetLength(1); i++)
                    {
                        if (_map[0, i] == 0)
                        {
                            _entryPoint =(0,i);
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

        private bool IsValidMove(int[,] grid, bool[,] visited, int row, int col, int total_rows, int total_columns)
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
            SearchPath();
        }

        private void SearchPath()
        {
            int rows = _map.GetLength(0);
            int columns = _map.GetLength(1);

            bool[,] visited = new bool[rows, columns];

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue(EntryPoint);
            visited[EntryPoint.Item1,EntryPoint.Item2] = true;

            _directions = new Vector2[rows, columns];

            Vector2[] directions = [
                new Vector2(-1,0),
                new Vector2(0,1),
                new Vector2(1,0),
                new Vector2(0,-1),
                ];
            while (queue.Count > 0)
            {
                var (row, col) = queue.Dequeue();



                foreach (Vector2 direction in directions)
                {
                    int newRow = (int)(row + direction.Y);
                    int newCol = (int)(col + direction.X);

                    if(IsValidMove(_map, visited, newRow, newCol, rows, columns))
                    {
                        visited[newRow, newCol] = true;
                        queue.Enqueue((newRow, newCol));
                        _directions[row, col] = direction;
                    }
                }

            }


        }
    }
}
