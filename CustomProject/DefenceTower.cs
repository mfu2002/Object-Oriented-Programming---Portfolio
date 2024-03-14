using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class DefenceTower : GameObject
    {
        private int _attackStrength = 1;
        private float _cooldown;
        private List<Enemy> _enemies;
        private IEnumerable<Enemy> targets;

        private float _range = 1.5f;

        private int _laserCapacity = 1;

        public int LaserCapacity
        {
            get { return _laserCapacity; }
            set { _laserCapacity = value; }
        }



        public float Range
        {
            get { return _range; }
            set { _range = value; }
        }


        public int AttackStrength
        {
            get { return _attackStrength; }
            set { _attackStrength = value; }
        }

        private int _attackDelay = 1;

        public int AttackDelay
        {
            get { return _attackDelay; }
            set { _attackDelay = value; }
        }

        public DefenceTower(Vector2 location, List<Enemy> enemies)
        {
            Location = location;
            _enemies =  enemies;

        }

        

        public void Attack()
        {
            if (_cooldown > 0) { return; }
            targets = _enemies.Where((enemy) =>(enemy.GridLocation - GridLocation).LengthSquared() < Range).Take(LaserCapacity);
            foreach (Enemy enemyWithinRange in targets)
            {
                enemyWithinRange.DealDamage(AttackStrength);
                _cooldown = AttackDelay;
            }
        }


        public override void Update(float deltaTime)
        {
            _cooldown -= deltaTime;
            if (_cooldown < 0) _cooldown = 0;
            Attack();
        }

        public override void GetDrawInstructions(List<DrawInstructions> drawInstructions)
        {
            drawInstructions.Add(new DrawInstructions(() => SplashKit.FillCircle(Color.Pink, Location.X, Location.Y, 20), 3));
            foreach (Enemy target in targets)
            {
                drawInstructions.Add(new DrawInstructions(() => SplashKit.DrawLine(Color.Red, Location.X, Location.Y, target.Location.X, target.Location.Y), 3));

            }

        }

    }
}
