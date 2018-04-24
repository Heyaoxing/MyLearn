using EventBus.Handlers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBus.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        /// <summary>
        /// 定义锁对象
        /// </summary>
        private static readonly object LockObj = new object();

        private readonly ConcurrentDictionary<string, List<string>> _eventAndHandlerMapping;

        public InMemoryEventStore()
        {
            _eventAndHandlerMapping = new ConcurrentDictionary<string, List<string>>();
        }
        public void AddRegister<T, TH>() where T : IEventData where TH : IEventHandler
        {
            AddRegister(typeof(T).Name, typeof(TH).Name);
        }


        //public void AddActionRegister<T>(Action<T> action) where T : IEventData
        //{
        //    var actionHandler = new ActionEventHandler<T>(action);

        //    AddRegister(typeof(T), actionHandler.GetType());
        //}

        public void AddRegister(string eventData, string eventHandler)
        {
            lock (LockObj)
            {
                if (!HasRegisterForEvent(eventData))
                {
                    var handlers = new List<string>();
                    _eventAndHandlerMapping.TryAdd(eventData, handlers);
                }

                if (_eventAndHandlerMapping[eventData].All(h => h != eventHandler))
                {
                    _eventAndHandlerMapping[eventData].Add(eventHandler);
                }
            }
        }

        public void RemoveRegister<T, TH>() where T : IEventData where TH : IEventHandler
        {
            var handlerToRemove = FindRegisterToRemove(typeof(T).Name, typeof(TH).Name);
            RemoveRegister(typeof(T).Name, handlerToRemove);
        }

        //public void RemoveActionRegister<T>(Action<T> action) where T : IEventData
        //{
        //    var actionHandler = new ActionEventHandler<T>(action);
        //    var handlerToRemove = FindRegisterToRemove(typeof(T), actionHandler.GetType());
        //    RemoveRegister(typeof(T), handlerToRemove);
        //}

        public void RemoveRegister(string eventData, string eventHandler)
        {
            if (eventHandler != null)
            {
                lock (LockObj)
                {
                    _eventAndHandlerMapping[eventData].Remove(eventHandler);
                    if (!_eventAndHandlerMapping[eventData].Any())
                    {
                        List<string> removedHandlers;
                        _eventAndHandlerMapping.TryRemove(eventData, out removedHandlers);
                    }
                }
            }
        }

        private string FindRegisterToRemove(string eventData, string eventHandler)
        {
            if (!HasRegisterForEvent(eventData))
            {
                return null;
            }

            return _eventAndHandlerMapping[eventData].FirstOrDefault(eh => eh == eventHandler);
        }

        //public bool HasRegisterForEvent<T>() where T : IEventData
        //{
        //    return _eventAndHandlerMapping.ContainsKey(T.ToString());
        //}

        public bool HasRegisterForEvent(string eventData)
        {
            return _eventAndHandlerMapping.ContainsKey(eventData);
        }

        public IEnumerable<string> GetHandlersForEvent<T>() where T : IEventData
        {
            return GetHandlersForEvent(typeof(T).Name);
        }

        public IEnumerable<string> GetHandlersForEvent(string eventData)
        {
            if (HasRegisterForEvent(eventData))
            {
                return _eventAndHandlerMapping[eventData];
            }

            return new List<string>();
        }

        public string GetEventTypeByName(string eventName)
        {
            return _eventAndHandlerMapping.Keys.FirstOrDefault(eh => eh == eventName);
        }

        public bool IsEmpty => !_eventAndHandlerMapping.Keys.Any();

        public void Clear() => _eventAndHandlerMapping.Clear();
    }
}
