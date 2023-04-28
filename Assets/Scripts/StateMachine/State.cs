using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class State<T>
    {
        protected StateMachine<T> _stateMachine;
        protected T _context;

        internal void SetContext(StateMachine<T> stateMachine, T context)
        {
            this._stateMachine = stateMachine;
            this._context = context;
        }
        
        public virtual void OnEnter()
        {}

        public abstract void Update();

        public virtual void OnExit()
        {}
    }
}
