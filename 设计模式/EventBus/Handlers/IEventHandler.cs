using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Handlers
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<TEventData>: IEventHandler where TEventData:IEventData
    {
        /// <summary>
        /// 处理事件源
        /// </summary>
        /// <param name="eventData"></param>
        void Handler(TEventData eventData);
    }
}
