using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachines
{
    public interface IEnterActionSyntax<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {
        IOnSyntax<TEvent, TState> OnEnterState(Action action);
    }
}
