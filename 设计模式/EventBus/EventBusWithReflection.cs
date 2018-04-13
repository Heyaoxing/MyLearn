using EventBus.Handlers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EventBus
{
    public class EventBusWithReflection
    {
        private static readonly EventBusWithReflection Singleton = new EventBusWithReflection();
        private readonly ConcurrentDictionary<Type, List<Type>> _eventHandleMapping;
        private EventBusWithReflection()
        {
            _eventHandleMapping = new ConcurrentDictionary<Type, List<Type>>();
            MapEventToHandler();
        }

        public static EventBusWithReflection singleton
        {
            get { return Singleton; }
        }

        /// <summary>
        ///通过反射，将事件源与事件处理绑定
        /// </summary>
        private void MapEventToHandler()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly == null) return;

            foreach (Type type in assembly.GetTypes())
            {
                var handle = type.GetInterface("IEventHandler`1");
                if (handle == null) continue;
                var genericArguments = handle.GetGenericArguments();
                if (genericArguments == null|| genericArguments.Length==0) continue;
                var eventData = genericArguments[0];
                if (_eventHandleMapping.ContainsKey(eventData))
                {
                    List<Type> handles = _eventHandleMapping[eventData];
                    handles.Add(type);
                    _eventHandleMapping[eventData] = handles;
                }
                else
                {
                    _eventHandleMapping.TryAdd(eventData, new List<Type>() { type });
                }

            }
        }

        /// <summary>
        /// 手动绑定事件源与事件处理
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventHandler"></param>
        public void Register<TEventData>(Type eventHandler)
        {
            if (!_eventHandleMapping.Keys.Contains(typeof(TEventData))) return;

            var eventDatas = _eventHandleMapping[typeof(TEventData)];
            var eventdata = eventHandler.GetGenericArguments()[0];
            if (eventDatas.Contains(eventdata))
            {
                List<Type> handles = _eventHandleMapping[eventdata];
                handles.Add(eventHandler);
                _eventHandleMapping[eventdata] = handles;
            }
            else
            {
                _eventHandleMapping.TryAdd(eventdata, new List<Type>() { eventHandler });
            }
        }


        /// <summary>
        /// 手动解除事件源与事件处理的绑定
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventHandler"></param>
        public void UnRegister<TEventData>(Type eventHandler)
        {
            if (!_eventHandleMapping.Keys.Contains(typeof(TEventData))) return;
            List<Type> handles = _eventHandleMapping[typeof(TEventData)];
            if (handles.Contains(eventHandler))
            {
                handles.Remove(eventHandler);
                _eventHandleMapping[typeof(TEventData)] = handles;
            }


        }

        /// <summary>
        /// 根据事件源触发绑定的事件处理
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventData"></param>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            if (!_eventHandleMapping.ContainsKey(typeof(TEventData))) return;
            List<Type> handles = _eventHandleMapping[typeof(TEventData)];
            if (handles == null || handles.Count <= 0) return;

            foreach (var handle in handles)
            {
                var methods = handle.GetMethod("Handler");
                if (methods == null) continue;

                var instance = (IEventHandler)Activator.CreateInstance(handle);
                methods.Invoke(instance, new object[] { eventData });
            }
        }
    }
}
