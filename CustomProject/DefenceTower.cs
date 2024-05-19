using CustomProject.Foe;
using SplashKitSDK;
using System.Numerics;
namespace CustomProject
{
    public class DefenceTower : GameObject
    {
        /// <summary>
        /// Cooldown between each fire.
        /// </summary>
        private float _cooldown;

        /// <summary>
        /// reference to the list on enemies currently in the game. 
        /// </summary>
        private readonly List<Enemy> _enemies;

        /// <summary>
        /// List of enemies the tower is targetting. 
        /// </summary>
        private IEnumerable<Enemy>? targets;


        /// <summary>
        /// The amount the capacity will be incremented at next upgrade. 
        /// </summary>
        public int UpgradeLaserCapacityIncrement { get; private set; } = 1;
        /// <summary>
        /// The amount the firing speed will be incremented at next upgrade. 
        /// </summary>
        public float UpgradeSpeedIncrement { get; private set; } = 10f;
        /// <summary>
        /// The amount the attack strength will be incremented at next upgrade. 
        /// </summary>
        public float UpgradeStrengthIncrement { get; private set; } = 10f;

        /// <summary>
        /// The amount the range will be incremented at next upgrade. 
        /// </summary>
        public float UpgradeRangeIncrement { get; private set; } = 1.5f;



        /// <summary>
        /// Current laser capacity. 
        /// </summary>
        public int LaserCapacity { get; private set; }

        /// <summary>
        /// Whether the tower is already erected
        /// </summary>
        public bool Built { get; set; }

        /// <summary>
        /// current range of the tower. 
        /// </summary>

        public float Range { get; private set; }

        /// <summary>
        /// Current Attack strength
        /// </summary>
        public float AttackStrength { get; private set; }

        /// <summary>
        /// Current firing speed. 
        /// </summary>
        public float AttackSpeed { get; private set; }

        /// <summary>
        /// Current Tower level
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Cost to upgrade the tower to the next level.
        /// </summary>
        public int UpgradeCost
        {
            get { return Level * 30; }
        }

        /// <summary>
        /// Defence tower that will attack the enemies
        /// </summary>
        /// <param name="location">Location of the tower</param>
        /// <param name="enemies">Reference to the list of enemies currently in the game.</param>
        public DefenceTower(Vector2 location, List<Enemy> enemies)
        {
            Location = location;
            _enemies = enemies;

        }
        /// <summary>
        /// Upgrades the tower and sets the next upgrade increments. 
        /// </summary>
        /// <returns>The cost of the upgrade</returns>
        private int Upgrade()
        {
            int cost = UpgradeCost;
            Range += UpgradeRangeIncrement;
            AttackStrength += UpgradeStrengthIncrement;
            AttackSpeed += UpgradeSpeedIncrement;
            LaserCapacity += UpgradeLaserCapacityIncrement;
            Level += 1;
            if (Level % 5 == 0)
            {
                UpgradeLaserCapacityIncrement = 1;

            }
            else
            {
                UpgradeLaserCapacityIncrement = 0;
            }
            if (!Built)
            {
                Built = true;
                UpgradeRangeIncrement = 0.2f;
                UpgradeStrengthIncrement = 1f;
                UpgradeSpeedIncrement = 2;

                return 50;
            }
            return cost;
        }

        /// <summary>
        /// Handles User key input
        /// </summary>
        /// <param name="money">Current user balance</param>
        /// <returns>Player money consumed</returns>
        public int HandleUserInput(int money)
        {
            if (money < UpgradeCost) { return 0; }

            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                return Upgrade();
            }
            return 0;
        }

        /// <summary>
        /// Inflicts damage on the enemy
        /// </summary>
        public void Attack()
        {
            if (_cooldown > 0) { return; }
            targets = _enemies.Where((enemy) => (enemy.GridLocation - GridLocation).LengthSquared() < Range).Take(LaserCapacity);
            foreach (Enemy enemyWithinRange in targets)
            {
                enemyWithinRange.DealDamage(AttackStrength);
                _cooldown = 1 / AttackSpeed;
            }
        }


        public override void Update(float deltaTime)
        {
            if (!Built) { return; }
            _cooldown -= deltaTime;
            if (_cooldown < 0) _cooldown = 0;
            Attack();
        }

        public override void GetDrawInstructions(List<DrawInstructions> drawInstructions)
        {
            drawInstructions.Add(new DrawInstructions(() =>
            {
                SplashKit.FillCircle(Color.Pink, Location.X, Location.Y, 20);
            }, 3));
            if (targets != null)
            {
                foreach (Enemy target in targets!)
                {
                    drawInstructions.Add(new DrawInstructions(() => SplashKit.DrawLine(Color.Red, Location.X, Location.Y, target.Location.X, target.Location.Y), 3));
                }
            }

        }

    }
}
