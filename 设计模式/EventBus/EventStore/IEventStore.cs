using EventBus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.EventStore
{
    public interface IEventStore
    {
        void AddRegister<T, TH>() where T : IEventData where TH : IEventHandler;
        void AddRegister(string eventData, string eventHandler);
      //  void AddActionRegister<T>(Action<T> action) where T : IEventData;
        void RemoveRegister<T, TH>() where T : IEventData where TH : IEventHandler;
   //     void RemoveActionRegister<T>(Action<T> action) where T : IEventData;
        void RemoveRegister(string eventData, string eventHandler);
     //   bool HasRegisterForEvent<T>() where T : IEventData;
        bool HasRegisterForEvent(string eventData);
        IEnumerable<string> GetHandlersForEvent<T>() where T : IEventData;
        IEnumerable<string> GetHandlersForEvent(string eventData);

        string GetEventTypeByName(string eventName);
        bool IsEmpty { get; }
        void Clear();
    }
}
