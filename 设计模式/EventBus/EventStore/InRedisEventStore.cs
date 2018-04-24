using System;
using System.Collections.Generic;
using System.Text;
using Common;
using EventBus.Handlers;

namespace EventBus.EventStore
{
    public class InRedisEventStore : IEventStore
    {
        public bool IsEmpty => throw new NotImplementedException();
        private static readonly object _lock = new object();

        public void AddRegister<T, TH>()
            where T : IEventData
            where TH : IEventHandler
        {
        }

        public void AddRegister(string eventData, string eventHandler)
        {
            lock (_lock)
            {
                if (!RedisUtil.KeyExists(eventData))
                {
                    var list = RedisUtil.ListRange<string>(eventData);
                    list.Add(eventHandler);
                    RedisUtil.SetStringKey(eventData, list);
                }
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public string GetEventTypeByName(string eventName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetHandlersForEvent<T>() where T : IEventData
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetHandlersForEvent(string eventData)
        {
            var types = new List<string>();
            if (RedisUtil.KeyExists(eventData))
            {
                 types = RedisUtil.GetStringKey<List<string>>(eventData);
            }
            return types;
        }

        public bool HasRegisterForEvent<T>() where T : IEventData
        {
            throw new NotImplementedException();
        }

        public bool HasRegisterForEvent(string eventData)
        {
            throw new NotImplementedException();
        }

        public void RemoveRegister<T, TH>()
            where T : IEventData
            where TH : IEventHandler
        {
            throw new NotImplementedException();
        }

        public void RemoveRegister(string eventData, string eventHandler)
        {
            throw new NotImplementedException();
        }
    }
}
