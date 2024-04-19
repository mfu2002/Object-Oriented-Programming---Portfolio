using CustomProject.Foe;
using SplashKitSDK;
using System.Numerics;
namespace CustomProject
{
    public class DefenceTower : GameObject
    {
        private float _cooldown;
        private readonly List<Enemy> _enemies;
        private IEnumerable<Enemy>? targets;


        public int UpgradeLaserCapacityIncrement { get; private set; } = 1;
        public float UpgradeSpeedIncrement { get; private set; } = 10f;
        public float UpgradeStrengthIncrement { get; private set; } = 10f;
        public float UpgradeRangeIncrement { get; private set; } = 1.5f;


        public int LaserCapacity { get; private set; }

        public bool Built { get; set; }

        public float Range { get; private set; }

        public float AttackStrength { get; private set; }

        public float AttackSpeed { get; private set; }





        public int Level { get; private set; }


        public int UpgradeCost
        {
            get { return Level * 30; }
        }

        public DefenceTower(Vector2 location, List<Enemy> enemies)
        {
            Location = location;
            _enemies = enemies;

        }

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


        public int HandleUserInput(int money)
        {
            if (money < UpgradeCost) { return 0; }

            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                return Upgrade();
            }
            return 0;
        }


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
