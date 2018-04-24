using Common;
using EventBus;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace EventDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBus.EventBus.Singleton.RegisterAllEventHandlerFromAssembly(Assembly.GetExecutingAssembly());
            FishingEventData eventData = new FishingEventData();
            eventData.FishingMan = new FishingMan("小明");
            while (true)
            {
                eventData.FishType = (FishType)new Random().Next(0, 5);
                if (new Random().Next(0, 10) % 2 == 0)
                {
                    EventBus.EventBus.Singleton.Trigger<FishingEventData>(eventData);
                }

                if (eventData.FishingMan.Count > 5) break;
            }

            Console.WriteLine("结束");
            Console.Read();
        }
    }
}
