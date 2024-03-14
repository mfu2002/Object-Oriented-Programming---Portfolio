using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public abstract class GameObject : IDraw
    {

        public GameObject() { }

		private Vector2 _location;

		public Vector2 Location
		{
			get { return _location; }
			set { _location = value; }
		}


        public Vector2 GridLocation
        {
            get
            {
                return Location / Game.TILE_WIDTH;
            }
        }

        public virtual void Update(float deltaTime){ }

        public abstract void GetDrawInstructions(List<DrawInstructions> instructions);
    }
}
