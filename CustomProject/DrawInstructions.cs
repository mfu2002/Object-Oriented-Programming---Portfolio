using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject
{
    public class DrawInstructions
    {
		private int _z;

		public int Z
		{
			get { return _z; }
		}

		private Action _draw;

		public Action Draw
		{
			get { return _draw; }
		}

		public DrawInstructions(Action draw, int z)
		{
			_draw = draw;
			_z = z;	
		}

	}
}
