using AForge.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.遗传算法
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            SimpleExample f = new SimpleExample();
         //   f.Mode =OptimizationFunction1D.Modes.Minimization;//寻求方程最小值
            f.Mode =OptimizationFunction1D.Modes.Maximization;//寻求方程最小值
            Population population = new Population(40, new BinaryChromosome(32), f, new RouletteWheelSelection());
            population.RunEpoch();
            double goodX = f.Translate(population.BestChromosome);
            Console.WriteLine("Best Chromosome ===  >{0}", goodX);
            Console.WriteLine("Best Result ===  >{0}", f.OptimizationFunction(goodX));
            Console.WriteLine("Over!");
            Console.ReadLine();
        }
    }
}
