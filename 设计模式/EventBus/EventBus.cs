using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EventBus.EventStore;
using EventBus.Handlers;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        private static readonly EventBus singleton = new EventBus();

        private ContainerBuilder builder;
        private IContainer container;
        private IEventStore eventStore;
        private EventBus()
        {
            builder = new ContainerBuilder();
            eventStore = new InRedisEventStore();
        }

        public static EventBus Singleton
        {
            get { return singleton; }
        }
        /// <summary>
        /// 注册所有
        /// </summary>
        public void Register()
        {
            throw new NotImplementedException();
        }

        public void Register<TEventData>(IEventHandler eventHandler) where TEventData : IEventData
        {
            throw new NotImplementedException();
        }

        public void Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            throw new NotImplementedException();
        }

        public void Register(Type eventType, Type handler)
        {

        }

        public void RegisterAllEventHandlerFromAssembly(Assembly assembly)
        {
            if (assembly == null) return;
            foreach (Type type in assembly.GetTypes())
            {
                var handle = type.GetInterface("IEventHandler`1");
                if (handle == null) continue;
                var genericArguments = handle.GetGenericArguments();
                if (genericArguments == null || genericArguments.Length == 0) continue;
                var eventData = genericArguments[0];
                string resolveName = eventData.Name + "_" + type.Name;
                builder.RegisterType(type).Named<IEventHandler>(resolveName).InstancePerLifetimeScope();
                eventStore.AddRegister(eventData.Name, resolveName);
            }
            container = builder.Build();
        }

        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            var _eventHandleMapping = eventStore.GetHandlersForEvent(eventData.GetType().Name);
            if (_eventHandleMapping == null) return;

            foreach (var handle in _eventHandleMapping)
            {
                var eventHandler = container.ResolveNamed<IEventHandler>(handle);
                var handler = eventHandler as IEventHandler<TEventData>;
                handler?.Handler(eventData);
            }
        }

        public void Trigger<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData
        {
            throw new NotImplementedException();
        }

        public Task TriggerAsycn<TEventData>(Type eventHandlerType, TEventData eventData) where TEventData : IEventData
        {
            throw new NotImplementedException();
        }

        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            throw new NotImplementedException();
        }

        public void UnRegister<TEventData>(Type handlerType) where TEventData : IEventData
        {
            throw new NotImplementedException();
        }

        public void UnRegisterAll<TEventData>() where TEventData : IEventData
        {
            throw new NotImplementedException();
        }
    }
}
