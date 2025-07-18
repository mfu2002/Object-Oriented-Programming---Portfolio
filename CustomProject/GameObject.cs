﻿using System.Numerics;

namespace CustomProject
{
    public abstract class GameObject() : IDraw
    {
        /// <summary>
        /// Location of the object on screen.
        /// </summary>
        public Vector2 Location { get; set; }

        /// <summary>
        /// Location of the object in the game grid
        /// </summary>        
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
