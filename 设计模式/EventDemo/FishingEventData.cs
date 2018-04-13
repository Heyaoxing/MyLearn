using EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventDemo
{
    public class FishingEventData: EventData
    {
        public FishingMan FishingMan { set; get; }
        public FishType FishType { set; get; }
    }
}
