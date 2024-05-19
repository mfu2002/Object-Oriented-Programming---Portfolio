using SplashKitSDK;
using System.Numerics;

namespace CustomProject.Foe
{
    /// <summary>
    /// The enemy the player aims to destroy.
    /// </summary>
    /// <param name="health">Initial health of the enemy</param>
    /// <param name="speed">Speed multiplier of the enemy</param>
    /// <param name="reward">Reward the player will receive for destroying it.</param>
    public abstract class Enemy(float health, float speed, int reward) : GameObject
    {
        /// <summary>
        /// Inital health of the enenmy when it was spawned.
        /// </summary>
        private readonly float _initialHealth = health;


        /// <summary>
        /// Unit vector pointing in the direction the enemy should face.
        /// </summary>
        private Vector2 _direction = new Vector2(0, 1);

        /// <summary>
        /// The next tile the enemy should aim to march to.
        /// </summary>
        private Vector2 _targetTile;

        /// <summary>
        /// Current health of the enemy. Also example of encapsulation where health of the enemy cannot be altered directly.
        /// </summary>
        public float Health { get; private set; } = health;

        /// <summary>
        /// Reward/curency unit received for killing this enemy.
        /// </summary>
        public int Reward { get; set; } = reward;

        /// <summary>
        /// speed multiplier. 
        /// </summary>
        public float SpeedFactor { get; set; } = speed;
        public Vector2 Direction
        {
            get { return _direction; }
            set
            {
                // Make sure direction is a unit vector
                _direction = value / value.Length();

            }
        }

        /// <summary>
        /// Speed with direction. 
        /// </summary>
        public Vector2 Velocity
        {
            get
            {
                return Direction * SpeedFactor;
            }
        }


        /// <summary>
        /// Updates the target tile of the enemy. 
        /// </summary>
        /// <param name="pathFinder">Object that the enemy should use the find its next direction.</param>
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
            // Updates the direction with a smoothness factor.
            Vector2 desiredDirection = _targetTile - Location + new Vector2(Game.TILE_WIDTH, Game.TILE_WIDTH) / 2;

            Direction = Vector2.Lerp(Direction, desiredDirection, 0.2f * deltaTime);

            ///x = v * dt
            Location += Velocity * deltaTime;
        }

        public override void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            if (Health < _initialHealth)
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

        /// <summary>
        /// Deals damage on the enemy.
        /// </summary>
        /// <param name="damage"></param>
        public void DealDamage(float damage)
        {
            Health -= damage;
        }
    }
}
