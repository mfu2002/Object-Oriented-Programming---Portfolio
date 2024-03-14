using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class Enemy : GameObject
    {
        private int _initialHealth = 5;
        private int _health = 5;

        private int _level = 1;

        private Vector2 _targetTile;

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        private int _reward = 10;

        public int Reward
        {
            get { return _reward; }
            private set { _reward = value; }
        }


        private int _speedFactor = 20;

        public int SpeedFactor
        {
            get { return _speedFactor; }
            set { _speedFactor = value; }
        }

        private Vector2 _direction = new Vector2(0, 1);

        public Vector2 Direction
        {
            get { return _direction; }
            set
            {
                // Make sure direction is a unit vector
                _direction = value / value.Length();
                
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return Direction * SpeedFactor;
            }
        }


        public void UpgradeSpeed()
        {
            SpeedFactor += 10;
            Reward += 10;
        }

        public void Navigate(PathFinder pathFinder)
        {
            int currLocX = (int)GridLocation.X;
            int currLocY = (int)GridLocation.Y;
            Vector2 nextDirection = pathFinder.GetDirection(currLocX, currLocY);
            int nextLocX = (int)(currLocX + nextDirection.X);
            int nextLocY = (int)(currLocY + nextDirection.Y);
            _targetTile = new Vector2(nextLocX, nextLocY) * Game.TILE_WIDTH;
        }


        public override void Update(float deltaTime)
        {
            Vector2 desiredDirection = _targetTile - Location + new Vector2(Game.TILE_WIDTH, Game.TILE_WIDTH) /2;

            Direction = Vector2.Lerp(Direction, desiredDirection, 0.2f * deltaTime);

            Location += Velocity * deltaTime;
        }

        public override void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            instructions.Add(new DrawInstructions(() => SplashKit.FillCircle(Color.Red, Location.X, Location.Y, 10), 2));

            if(Health < _initialHealth)
            {
                instructions.Add(new DrawInstructions(() =>
                {
                    double rectWidth = 10;
                    double rectHeight = 3;
                    double rectBorder = 1;
                    SplashKit.FillRectangle(Color.Black, Location.X - rectWidth / 2 - rectBorder, Location.Y - rectHeight - 15 - rectBorder, rectWidth + rectBorder * 2, rectHeight + rectBorder * 2);
                    SplashKit.FillRectangle(Color.Red, Location.X - rectWidth / 2, Location.Y - rectHeight - 15, rectWidth * Health / _initialHealth, rectHeight);
                }, 3));
            }

        }


        public void DealDamage(int damage)
        {
            Health -= damage;
        }
    }
}
