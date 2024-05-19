using CustomProject.Foe;
using System.Numerics;

namespace CustomProject
{
    /// <summary>
    /// Responsible for coordinating the enemy with the map. 
    /// </summary>
    /// <param name="mapSchema">The game map layout</param>
    internal class EnemyCommander(int[,] mapSchema) : IDraw
    {

        /// <summary>
        /// Helps establish the path from the start to the end of the map.
        /// </summary>
        private readonly PathFinder _pathFinder = new PathFinder(mapSchema);
        /// <summary>
        /// Game level
        /// </summary>
        private int _stage = 0;
        /// <summary>
        /// cooldown between each troops
        /// </summary>
        private float _cooldown = 5;

        /// <summary>
        /// reset value for the cooldown. 
        /// </summary>
        private float _delay = 10;

        /// <summary>
        /// Number of enemies in each troop. 
        /// </summary>
        private int _troopCount = 7;

        /// <summary>
        /// List of enemies currently in game. 
        /// </summary>
        public List<Enemy> Enemies { get; } = [];

        public void GetDrawInstructions(List<DrawInstructions> instructions)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.GetDrawInstructions(instructions);
            }
        }

        /// <summary>
        /// Removes dead enemies and calculates the total reward.
        /// </summary>
        /// <returns>The total reward for the dead enemies.</returns>
        public int CheckDeadEnemies()
        {
            int unclaimedReward = 0;
            IEnumerable<Enemy> deadEnemies = Enemies.Where((enemy) => enemy.Health <= 0);

            foreach (Enemy enemy in deadEnemies)
            {
                unclaimedReward += enemy.Reward;
            }

            Enemies.RemoveAll((enemy) => enemy.Health <= 0);
            return unclaimedReward;
        }

        /// <summary>
        /// Checks whether any enemy has reached the last tile and removes them from the list.
        /// </summary>
        /// <returns>Returns the number of enemies that have reached the end</returns>
        public int CheckVictoriousEnemies()
        {
            int enemyCount = 0;
            List<Enemy> victoryEnemies = [];

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
                if (enemy.Health > 0)
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

            UpdateEnemies(deltaTime);
            CreateEnemies();

        }

        /// <summary>
        /// Creates the enemies on the field.
        /// </summary>
        private void CreateEnemies()
        {
            if (_cooldown > 0 || Enemies.Count > 0) { return; }
            _stage++;
            Random random = new Random();
            IEnemyFactory enemyFactory = new WeightedEnemyFactory(_stage, random);
            for (int i = 0; i < _troopCount; i++)
            {
                Enemy basicEnemy = enemyFactory.CreateEnemy();
                float varianceX = random.Next(80) / 100f + 0.1f;
                float varianceY = random.Next(80) / 100f + 0.1f;
                basicEnemy.Location = new Vector2((_pathFinder.EntryPoint.Item1 + varianceX) * Game.TILE_WIDTH, varianceY * Game.TILE_WIDTH);
                Enemies.Add(basicEnemy);
            }
            _troopCount += 7;
            _cooldown = _delay;
        }
    }
}
