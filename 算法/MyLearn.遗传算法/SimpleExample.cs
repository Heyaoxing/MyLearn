using AForge.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;

namespace MyLearn.遗传算法
{
    public class SimpleExample : OptimizationFunction1D
    {
        public SimpleExample() : base(new AForge.Range(0, 100))
        {
        }

        public override double OptimizationFunction(double x)
        {
            return Math.Sqrt(x) + Math.Sin(x / 23) * 30;
        }
    }
}
