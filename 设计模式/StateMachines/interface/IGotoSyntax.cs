using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachines
{
    public interface IGotoSyntax<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {
        IOnSyntax<TEvent, TState> Goto(TState state);
    }
}
