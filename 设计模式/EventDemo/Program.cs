using EventBus;
using System;

namespace EventDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            FishingEventData fishingEventData = new FishingEventData();
            fishingEventData.FishingMan = new FishingMan("张杰");
            while (true)
            {
                if (new Random().Next() % 2 == 0)
                {
                    EventBusWithReflection.singleton.Trigger(fishingEventData);
                }
                fishingEventData.FishType = (FishType)new Random().Next(0, 5);
                if (fishingEventData.FishingMan.Count >= 5) break;
            }

            Console.WriteLine("结束");
            Console.Read();
        }
    }
}
