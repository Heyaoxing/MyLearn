using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachines
{
    public interface ITransition<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {
        TState From { set; get; }
        TState To { set; get; }
        TEvent Event { set; get; }
    }
}
