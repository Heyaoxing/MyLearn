using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace StateMachines
{
    /// <summary>
    /// 代表一个状态节点
    /// </summary>
    public interface IState<TEvent, TState> where TEvent : IComparable where TState : IComparable
    {
        TState StateId { get; set; }
        Action EnterAction { get; set; }
    }
}
