using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchSample.Composite
{
    public class Engine(int maxSpeed, int horsePower)
    {

        public int MaxSpeed { get; set; } = maxSpeed;
        public int HorsePower {  get; set; } = horsePower;
    }
}
