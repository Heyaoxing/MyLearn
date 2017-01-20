using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyLearn.Lambda;

namespace Learn.Console.Test
{
    class Program
    {
        private static readonly object _obj = new object();
        static volatile int current = 0;
        static EventWaitHandle wh = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            for (int i = 0; i <20; i++)
            {
                lock (_obj)
                {
                    current++;
                }
                ThreadPool.QueueUserWorkItem(tool,i);
                if (current >= 5)
                {
                    System.Console.WriteLine("等待通知");
                    wh.WaitOne(); // 等待通知
                }
            }
            System.Console.WriteLine("结束");
            System.Console.Read();
        }


        static void tool(object obj)
        {
            int index = (int)obj;
            Thread.CurrentThread.Name = "线程" + index;
            lock (_obj)
            {
                System.Console.WriteLine(Thread.CurrentThread.Name + "--线程数:" + current);
            }
            Thread.Sleep(10000);
            lock (_obj)
            {
                current--;
                if (current < 5)
                {
                    wh.Set(); // 唤醒
                }
            }
        }

        static void Explame()
        {
            Thread t = new Thread(Go);
            t.Name = "测试线程";
            t.Start();
            Thread.Sleep(1000);
            wh.Set();
            System.Console.WriteLine("结束");
            System.Console.Read();
        }

        static void Go()
        {
            try
            {
                List<string> a = new List<string>();
                List<string> b = new List<string>();
                a.Except(b);
               System.Console.WriteLine("waiting...");
                wh.WaitOne();
                System.Console.WriteLine("End");
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
            }
        }
    }
}
