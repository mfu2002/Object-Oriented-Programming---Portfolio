using SplashKitSDK;
using System.Numerics;

namespace CustomProject
{
    public abstract class Tile : GameObject
    {
        // Border width to indicate the tile is selected
        private const int BORDER_WIDTH = 2;

        /// <summary>
        /// Background colour
        /// </summary>
        private Color _groundColor;

        /// <summary>
        /// Identifies whether the player is allowed to select this tile.
        /// </summary>
        abstract public bool Selectable { get; }

        /// <summary>
        /// Identifies whether the tile is currently selected
        /// </summary>
        public bool Selected { get; set; }


        /// <summary>
        /// The ground of the playing field. 
        /// </summary>
        /// <param name="color">background colour</param>
        /// <param name="loc">location of the top left corner of the tile</param>
        protected Tile(Color color, Vector2 loc)
        {
            _groundColor = color;
            Location = loc;
        }

        /// <summary>
        /// Checks whether the point is within the tile bounds.
        /// </summary>
        /// <param name="point">The point vector in question</param>
        /// <returns>Whether the point is within bounds</returns>
        public bool IsAt(Vector2 point)
        {
            if (point.X < Location.X) return false;
            if (point.X > Game.TILE_WIDTH + Location.X) return false;
            if (point.Y < Location.Y) return false;
            if (point.Y > Game.TILE_WIDTH + Location.Y) return false;
            return true;
        }

        public override void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            instructions.Add(new DrawInstructions(() => SplashKit.FillRectangle(_groundColor, Location.X, Location.Y, Game.TILE_WIDTH, Game.TILE_WIDTH), 0));
            if (Selected)
            {
                instructions.Add(new DrawInstructions(() => SplashKit.DrawRectangle(Color.Black, Location.X - BORDER_WIDTH, Location.Y - BORDER_WIDTH, Game.TILE_WIDTH + BORDER_WIDTH * 2, Game.TILE_WIDTH + BORDER_WIDTH * 2), 1));
            }
        }

    }
}
