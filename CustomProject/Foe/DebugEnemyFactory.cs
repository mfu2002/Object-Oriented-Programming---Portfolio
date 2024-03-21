using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
