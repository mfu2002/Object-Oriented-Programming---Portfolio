namespace CustomProject.Foe
{

    /// <summary>
    /// The Factory method design pattern.
    /// </summary>
    internal interface IEnemyFactory
    {
        /// <summary>
        /// Goes through the logic on which enemy to create and outputs the enemy. 
        /// </summary>
        /// <returns>The created enemy</returns>
        public Enemy CreateEnemy();

    }
}
