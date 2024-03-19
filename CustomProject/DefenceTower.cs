using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CustomProject.Foe;
using System.Reflection.Metadata.Ecma335;
namespace CustomProject
{
    public class DefenceTower : GameObject
    {
        private float _attackStrength;
        private float _cooldown;
        private List<Enemy> _enemies;
        private IEnumerable<Enemy>? targets;
        private float _attackSpeed;

        private float upgradeRangeIncrement = 1.5f;
        private float upgradeStrengthIncrement = 10f;
        private float upgradeSpeedIncrement = 10f;
        private int upgradeLaserCapacityIncrement = 1;

        public int UpgradeLaserCapacityIncrement { get { return upgradeLaserCapacityIncrement; } }
        public float UpgradeSpeedIncrement { get { return upgradeSpeedIncrement; } }
        public float UpgradeStrengthIncrement { get { return upgradeStrengthIncrement; } }
        public float UpgradeRangeIncrement { get { return upgradeRangeIncrement; } }


        private float _range;

        private int _laserCapacity;

        public int LaserCapacity
        {
            get { return _laserCapacity; }
        }

        private bool _built;

        public bool Built
        {
            get { return _built; }
            set { _built = value; }
        }



        public float Range
        {
            get { return _range; }
        }


        public float AttackStrength
        {
            get { return _attackStrength; }
        }


        public float AttackSpeed
        {
            get { return _attackSpeed; }
        }

        public DefenceTower(Vector2 location, List<Enemy> enemies)
        {
            Location = location;
            _enemies = enemies;

        }

        private int _level;

        public int Level
        {
            get { return _level; }
        }


        public int UpgradeCost
        {
            get { return Level * 30; }
        }


        private int Upgrade()
        {
            int cost = UpgradeCost;
            _range += upgradeRangeIncrement;
            _attackStrength += upgradeStrengthIncrement;
            _attackSpeed += upgradeSpeedIncrement;
            _laserCapacity += upgradeLaserCapacityIncrement;
            _level += 1;
            if (Level % 5 == 0)
            {
                upgradeLaserCapacityIncrement = 1;

            }
            else
            {
                upgradeLaserCapacityIncrement = 0;
            }
            if (!Built)
            {
                Built = true;
                upgradeRangeIncrement = 0.2f;
                upgradeStrengthIncrement = 1f;
                upgradeSpeedIncrement = 2;

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
