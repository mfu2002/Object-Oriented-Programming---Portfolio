
namespace CustomProject.Foe
{
    public class WeightedEnemyFactory(int stage, Random random) : IEnemyFactory
    {
        private readonly Random _random = random;
        private readonly int _stage = stage;

        public Enemy CreateEnemy()
        {
            int enemyLimit;
            if (_stage < 5)
            {
                enemyLimit = 3;
            }
            else if (_stage < 7)
            {
                enemyLimit = 4;
            }
            else
            {
                enemyLimit = 5;
            }
            int randomNum = _random.Next(enemyLimit);
            Enemy enemy = randomNum switch
            {
                0 or 1 or 2 => new NormalEnemy(),
                3 => new FastEnemy(),
                _ => new StrongEnemy(),
            };
            return enemy;
        }
    }
}
