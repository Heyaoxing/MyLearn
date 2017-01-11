using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearn.Lambda;

namespace Learn.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            LearnDemo learnDemo = new LearnDemo();
            learnDemo.Process();
            System.Console.Read();
        }
    }
}
