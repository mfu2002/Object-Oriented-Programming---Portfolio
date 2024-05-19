namespace CustomProject
{
    public interface IDraw
    {
        /// <summary>
        /// Actions that need to be updated on each game cycle.
        /// </summary>
        /// <param name="deltaTime">change in time since last update.</param>
        public void Update(float deltaTime);

        /// <summary>
        /// Instructions on how to draw the object and its decorations. 
        /// </summary>
        /// <param name="drawInstructions">List where draw instructions will be added</param>
        public void GetDrawInstructions(List<DrawInstructions> drawInstructions);

    }
}
