
using System.Net.Http.Headers;

namespace CustomProject.Foe
{
    internal class DebugEnemyFactory : IEnemyFactory
    {
        public Enemy CreateEnemy()
        {
            return new DebugEnemy();
        }
    }
}
