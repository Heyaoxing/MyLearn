using EventBus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventDemo
{
    public class FishingEventHandler : IEventHandler<FishingEventData>
    {
        public void Handler(FishingEventData eventData)
        {
            eventData.FishingMan.Count++;
            Console.WriteLine("调到鱼了！！");
            Console.WriteLine($"总共钓到了{ eventData.FishingMan.Count}条,这次钓到是:{eventData.FishType}");
        }
    }
}
