using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachines
{
    public interface IOnSyntax<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {
        IGotoSyntax<TEvent, TState> On(TEvent @event);
    }
}
