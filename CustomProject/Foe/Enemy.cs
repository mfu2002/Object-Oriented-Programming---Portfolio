using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Foe
{
    public abstract class Enemy : GameObject
    {
        private float _initialHealth;
        private float _health;
        private int _reward;
        private float _speedFactor;

        private Vector2 _direction = new Vector2(0, 1);

        private Vector2 _targetTile;


        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }


        public int Reward
        {
            get { return _reward; }
            private set { _reward = value; }
        }



        public float SpeedFactor
        {
            get { return _speedFactor; }
            set { _speedFactor = value; }
        }


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

        protected Enemy(float health, float speed, int reward)
        {
            _initialHealth = health;
            _health = health;
            _speedFactor = speed;
            _reward = reward;
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


        public void DealDamage(float damage)
        {
            Health -= damage;
        }
    }
}
