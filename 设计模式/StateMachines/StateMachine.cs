using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachines
{
    public class StateMachine<TEvent, TState> : IStateMachine<TEvent, TState> where TState : IComparable where TEvent : IComparable
    {
        private struct EventMessage
        {
            public TEvent Event;
            public TaskCompletionSource<bool> Source;
        }

        public StateMachine(string name)
        {
            this.Name = name;
        }

        public string Name { set; get; }

        private IDictionary<TState, IDictionary<TEvent, ITransition<TEvent, TState>>> rules;

        private BlockingCollection<EventMessage> _eventChannel = new BlockingCollection<EventMessage>();

        public TState CurrentStateId { set; get; }

        private IDictionary<TState, Action> states;

        public Task<bool> Fire(TEvent @event)
        {
            //添加到队列，循环自己取来消费
            TaskCompletionSource<bool> source = new TaskCompletionSource<bool>();
            _eventChannel.Add(new EventMessage() { Event = @event, Source = source });
            return source.Task;
        }


        private void FireInternal(TEvent _event, TaskCompletionSource<bool> source)
        {
            var oldState = CurrentStateId;
            if (!rules.ContainsKey(oldState) || !rules[oldState].ContainsKey(_event))
            {
                source.SetResult(false);
                return;
            }

            var transition = rules[oldState][_event];
            if (transition.From.CompareTo(oldState) != 0 || transition.Event.CompareTo(_event) != 0)
            {
                source.SetResult(false);
                return;
            }

            CurrentStateId = transition.To;

            if (states.ContainsKey(oldState) && states[oldState] != null)
                states[oldState].Invoke();

            source.SetResult(true);
        }

        public void Start(TState stateId)
        {
            CurrentStateId = stateId;
            Thread thread = new Thread(Eventloop);
            thread.IsBackground = false;
            thread.Name = this.Name;
            thread.Start();

        }

        private void Eventloop()
        {
            while (true)
            {
                var eventchannel = _eventChannel.Take();
                FireInternal(eventchannel.Event, eventchannel.Source);
            }
        }




        public IEnterActionSyntax<TEvent, TState> In(TState state)
        {
            return new StateMachineBuilder<TEvent, TState>(this).In(state);
        }

        public IDictionary<TState, IDictionary<TEvent, ITransition<TEvent, TState>>> TransitionRules
        {
            get
            {
                if (rules == null)
                    rules = new Dictionary<TState, IDictionary<TEvent, ITransition<TEvent, TState>>>();
                return rules;
            }
            set { rules = value; }
        }

        public IDictionary<TState, Action> States
        {
            get
            {
                return states ?? (states = new Dictionary<TState, Action>());
            }
            set => states = value;
        }
    }
}
