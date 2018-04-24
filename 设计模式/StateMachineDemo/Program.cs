using StateMachines;
using System;
using System.Threading;

namespace StateMachineDemo
{
    class Program
    {
        static  void Main(string[] args)
        {
            Test();
            Console.Read();
        }

        static async void Test()
        {
            var machine = new StateMachine<int, int>("test1");
            int temp = 0;
            machine.In(0).OnEnterState(() =>
            {
                temp++;
                Console.WriteLine("test" + temp);
            }).On(1).Goto(6);

            machine.In(6).OnEnterState(() =>
            {
                temp++;
                Console.WriteLine("test" + temp);
            }).On(3).Goto(7);
           // machine.Start(0);
            machine.Fire(1);
            await machine.Fire(3);
            Console.WriteLine(machine.CurrentStateId);
        }
    }
}
