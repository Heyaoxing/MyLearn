using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus
{
    /// <summary>
    /// 事件描述
    /// </summary>
    public class EventData:IEventData
    {
        /// <summary>
        /// 事件时间
        /// </summary>
        public DateTime EventDate { set; get; } = DateTime.Now;
        /// <summary>
        /// 事件对象
        /// </summary>
        public Object EventObject { set; get; }
    }
}
