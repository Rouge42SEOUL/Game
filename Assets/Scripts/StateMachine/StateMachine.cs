using System;
using System.Collections.Generic;

namespace StateMachine
{
    public class StateMachine<T>
    {
        private T _context;

        private State<T> _currentState;
        public State<T> CurrentState => _currentState;

        private State<T> _previousState;
        public State<T> PreviousState => _previousState;

        private Dictionary<Type, State<T>> _states = new Dictionary<Type, State<T>>();

        public StateMachine(T context, State<T> initialState)
        {
            this._context = context;

            AddState(initialState);
            _currentState = initialState;
            _currentState.OnEnter();
        }

        public void AddState(State<T> state)
        {
            state.SetContext(this, _context);
            _states[state.GetType()] = state;
        }

        public void Update()
        {
            _currentState.Update();
        }

        public void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }

        public void ChangeState<T2>() where T2 : State<T>
        {
            if (typeof(T2) == _currentState.GetType())
                return;
            
            if (_currentState != null)
                _currentState.OnExit();
            _previousState = _currentState;
            _currentState = _states[typeof(T2)];
            _currentState.OnEnter();
        }
    }
}