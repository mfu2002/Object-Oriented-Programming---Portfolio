using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public interface IDraw
    {

        public void Update(float deltaTime);
        public void GetDrawInstructions(List<DrawInstructions> drawInstructions);

    }
}
