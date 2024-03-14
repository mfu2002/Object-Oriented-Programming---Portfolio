using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    internal class EnemyProcessor : IDraw
    {

        private PathFinder _pathFinder;

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



        public EnemyProcessor(int[,] mapSchema)
        {
            _pathFinder = new PathFinder(mapSchema);

            CreateEnemies();
        }

        public void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.GetDrawInstructions(instructions);
            }
        }

        public void Update(float deltaTime)
        {
            IEnumerable<Enemy> deadEnemies = Enemies.Where((enemy) => enemy.Health <= 0);

            foreach (Enemy enemy in deadEnemies)
            {
                UnclaimedReward += enemy.Reward;
            }

            Enemies.RemoveAll((enemy) => enemy.Health <= 0);

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


        private void CreateEnemies()
        {
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                Enemy basicEnemy = new Enemy();
                float variance = random.Next(80) / 100f + 0.1f;
                basicEnemy.Location = new Vector2((_pathFinder.EntryPoint.Item2 + variance) * Game.TILE_WIDTH, 0);
                _enemies.Add(basicEnemy);
            }


        }
    }
}
