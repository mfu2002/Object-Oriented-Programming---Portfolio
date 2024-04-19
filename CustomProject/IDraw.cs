namespace CustomProject
{
    public interface IDraw
    {

        public void Update(float deltaTime);
        public void GetDrawInstructions(List<DrawInstructions> drawInstructions);

    }
}
