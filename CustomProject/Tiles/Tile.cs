using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public abstract class Tile : GameObject
    {
        private Color _groundColor;
        



        private const int BORDER_WIDTH = 2;
        protected Tile(Color color)
        {
            _groundColor = color;
        }


        private bool _selected;

        abstract public bool Selectable { get; }
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }



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
                instructions.Add(new DrawInstructions(() => SplashKit.DrawRectangle(Color.Black, Location.X - BORDER_WIDTH, Location.Y - BORDER_WIDTH, Game.TILE_WIDTH + BORDER_WIDTH * 2, Game.TILE_WIDTH  +  BORDER_WIDTH * 2), 1));

            }
        }

    }
}
