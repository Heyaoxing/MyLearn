using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public interface IStateMachine<TEvent, TState> : IInSyntax<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {
        string Name { set; get; }
        Task<bool> Fire(TEvent @event);
        void Start(TState stateId);
        IEnterActionSyntax<TEvent, TState> In(TState state);
        IDictionary<TState, IDictionary<TEvent, ITransition<TEvent, TState>>> TransitionRules { set; get; }
        IDictionary<TState, Action> States { set; get; }
        TState CurrentStateId { set; get; }
    }
}
