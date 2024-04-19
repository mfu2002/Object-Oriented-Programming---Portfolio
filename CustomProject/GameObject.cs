using System.Numerics;

namespace CustomProject
{
    public abstract class GameObject() : IDraw
    {

        public Vector2 Location { get; set; }
        
        public Vector2 GridLocation
        {
            get
            {
                return Location / Game.TILE_WIDTH;
            }
        }

        public virtual void Update(float deltaTime) { }

        public abstract void GetDrawInstructions(List<DrawInstructions> instructions);
    }
}
