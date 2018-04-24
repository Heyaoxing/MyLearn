using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachines
{
    public interface IInSyntax<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {
        IEnterActionSyntax<TEvent, TState> In(TState state);
    }
}
