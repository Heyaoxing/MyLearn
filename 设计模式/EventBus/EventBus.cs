using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EventBus.Handlers;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        private static readonly EventBus singleton = new EventBus();

        private ContainerBuilder builder;
        private EventBus()
        {
            builder = new ContainerBuilder();
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
            throw new NotImplementedException();
        }

        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            throw new NotImplementedException();
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
