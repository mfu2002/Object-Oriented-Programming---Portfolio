using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class ConstructableTile : Tile
    {
        DefenceTower _tower;
        public DefenceTower Tower { get { return _tower; } set { _tower = value; } }

        public ConstructableTile() : base(Color.RGBColor(58, 239, 63))
        {
        }

        public override void GetDrawInstructions(List<DrawInstructions> drawInstructions)
        {
            base.GetDrawInstructions(drawInstructions);
            Tower?.GetDrawInstructions(drawInstructions);
        }

        public override void Update(float deltaTime)
        {
            Tower?.Update(deltaTime);
        }


        public override bool Selectable => true;

    }
}
