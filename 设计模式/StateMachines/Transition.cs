using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachines
{
    public class Transition<TEvent, TState> : ITransition<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {
        public TState From { set; get; }
        public TState To { set; get; }
        public TEvent Event { set; get; }

        public Transition(TState _from, TState _to, TEvent _event)
        {
            this.From = _from;
            this.To = _to;
            this.Event = _event;
        }
    }
}
