using CustomProject.Foe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    internal class EnemyCommander : IDraw
    {

        private PathFinder _pathFinder;
        private int _stage = 0;
        private float _cooldown = 5;
        private float _delay = 10;
        private int _toopCount = 7;

        private readonly List<Enemy> _enemies = new List<Enemy>();
        private int _unclaimedReward;

        public int UnclaimedReward
        {
            get { return _unclaimedReward; }
            set { _unclaimedReward = value; }
        }


        public List<Enemy> Enemies
        {
            get { return _enemies; }
        }



        public EnemyCommander(int[,] mapSchema)
        {
            _pathFinder = new PathFinder(mapSchema);
        }

        public int TakeReward()
        {
            int reward = UnclaimedReward;
            UnclaimedReward = 0;
            return reward;
        }

        public void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.GetDrawInstructions(instructions);
            }
        }

        private void HandleDeadEnemies(float deltaTime)
        {
            IEnumerable<Enemy> deadEnemies = Enemies.Where((enemy) => enemy.Health <= 0);

            foreach (Enemy enemy in deadEnemies)
            {
                UnclaimedReward += enemy.Reward;
            }

            Enemies.RemoveAll((enemy) => enemy.Health <= 0);

        }

        public int CheckVictoriousEnemies()
        {
            int enemyCount = 0;
            List<Enemy> victoryEnemies = new List<Enemy>();

            foreach (Enemy enemy in Enemies)
            {
                if ((int)enemy.GridLocation.X == _pathFinder.ExitPoint.Item1 && (int)enemy.GridLocation.Y == _pathFinder.ExitPoint.Item2)
                {
                    enemyCount += 1;
                    victoryEnemies.Add(enemy);
                }
            }
            foreach (Enemy enemy in victoryEnemies)
            {
                Enemies.Remove(enemy);
            }
            return enemyCount;
        }

        private void UpdateEnemies(float deltaTime)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.Health <= 0)
                {
                    Enemies.Remove(enemy);
                }
                else
                {

                    enemy.Navigate(_pathFinder);
                    enemy.Update(deltaTime);
                }
            }
        }

        public void Update(float deltaTime)
        {
            _cooldown -= deltaTime;
            if (_cooldown < 0) { _cooldown = 0; }

            HandleDeadEnemies(deltaTime);
            UpdateEnemies(deltaTime);
            CreateEnemies();

        }


        private void CreateEnemies()
        {
            if (_cooldown > 0 || Enemies.Count > 0) { return; }
            _stage++;
            Random random = new Random();
            IEnemyFactory enemyFactory = new WeightedEnemyFactory(_stage, random);
            for (int i = 0; i < _toopCount; i++)
            {
                Enemy basicEnemy = enemyFactory.CreateEnemy();
                float varianceX = random.Next(80) / 100f + 0.1f;
                float varianceY = random.Next(80) / 100f + 0.1f;
                basicEnemy.Location = new Vector2((_pathFinder.EntryPoint.Item1 + varianceX) * Game.TILE_WIDTH, varianceY);
                _enemies.Add(basicEnemy);
            }

            _toopCount += 7;
            _cooldown = _delay;

        }
    }
}
