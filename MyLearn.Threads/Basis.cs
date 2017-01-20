using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyLearn.Threads
{
    public class Basis
    {

        public void Application()
        {
            //设置主线程名称
            Thread.CurrentThread.Name = "主线程";
            //设置线程执行委托方法
            Thread t = new Thread(delegate() { Console.WriteLine("hello"); });
            //设置线程名称
            t.Name = "测试线程";
            //设置线程优先级
            t.Priority = ThreadPriority.Normal;
            //设置后台线程,默认为前台线程. 区别在于后台线程会随主线程同消亡,但前台线程则不会随主线程消亡
            t.IsBackground = true;
            //启动线程
            t.Start();

            //当前线程状态
            Console.WriteLine(t.ThreadState);
            Console.WriteLine(t.IsAlive);//线程start后到结束前,该值为true

            //阻塞等待直到线程结束
            t.Join();


            Thread.Sleep(0); // 释放CPU时间片
            Thread.Sleep(1000); // 休眠1000毫秒
            Thread.Sleep(TimeSpan.FromHours(1)); // 休眠1小时
            Thread.Sleep(Timeout.Infinite); // 休眠直到中断


            //任何（非阻止）线程要通过AutoResetEvent对象调用Set方法来释放一个被阻止的的线程。
            wh.Set(); // OK ——唤醒它
        }

        //等待句柄
        static EventWaitHandle wh = new AutoResetEvent(false);
        static void Go()
        {
            try
            {
                System.Console.WriteLine("waiting...");
                wh.WaitOne(); // 等待通知
                System.Console.WriteLine("End");
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception);
            }
        }
    }
}
