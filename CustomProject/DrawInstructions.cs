namespace CustomProject
{
    public class DrawInstructions(Action draw, int z)
    {
        public int Z { get; set; } = z;

        public Action Draw { get; } = draw;
    }
}
