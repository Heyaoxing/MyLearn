using System;
using System.Collections.Generic;

namespace StateMachines
{
    public class StateMachineBuilder<TEvent, TState> : IGotoSyntax<TEvent, TState>, IOnSyntax<TEvent, TState>, IEnterActionSyntax<TEvent, TState>, IInSyntax<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {

        private IStateMachine<TEvent, TState> _machine;
        private TEvent _event;
        private TState _from;
        public StateMachineBuilder(IStateMachine<TEvent, TState> machine)
        {
            _machine = machine;
        }
        public IEnterActionSyntax<TEvent, TState> In(TState state)
        {
            _from = state;
            if (!_machine.TransitionRules.ContainsKey(state))
            {
                _machine.TransitionRules.Add(state, new Dictionary<TEvent, ITransition<TEvent, TState>>());
            }
            return this;
        }

        public IGotoSyntax<TEvent, TState> On(TEvent @event)
        {
            _event = @event;
            return this;
        }

        public IOnSyntax<TEvent, TState> OnEnterState(Action action)
        {
            if (!_machine.States.ContainsKey(_from))
                _machine.States.Add(_from, action);
            return this;
        }

        public IOnSyntax<TEvent, TState> Goto(TState state)
        {
            if (!_machine.TransitionRules[_from].ContainsKey(_event))
            {
                _machine.TransitionRules[_from].Add(_event, new Transition<TEvent, TState>(_from, state, _event));
            }
            return this;
        }
    }
}
