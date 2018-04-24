using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachines
{
    public class State<TEvent, TState> : IState<TEvent, TState> where TEvent : IComparable where TState : IComparable
    {
        public TState StateId { get; set; }

        public State(TState stateId)
        {
            StateId = stateId;
        }

        public Action EnterAction { get; set; }
    }
}
