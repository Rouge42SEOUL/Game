
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class Player
    {
        protected StateMachine<Player> StateMachine;
        public override void GetHit() => _GetHit();
        internal Vector2 Movement => _movement;
        internal ActorStatSo Stat => stat;

        internal bool IsMoving
        {
            get
            {
                return _movement != Vector2.zero;
            }
        }
    }
    
    // Values or methods that other cannot use
    public partial class Player
    {
        private Vector2 _movement;
    }
    
    // body of MonoBehaviour
    public partial class Player : Actor
    {
        private void Start()
        {
            StateMachine = new StateMachine<Player>(this, new PlayerIdleState());
            StateMachine.AddState(new PlayerMoveState());
            StateMachine.AddState(new PlayerAttackState());
        }

        private void Update()
        {
            StateMachine.Update();
        }
        
        private void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }
    }
    
    // body of others
    public partial class Player
    {
        private void _GetHit()
        {
            throw new System.NotImplementedException();
        }

        protected override void Died()
        {
            throw new System.NotImplementedException();
        }

        private void OnMovement(InputValue value)
        {
            _movement = value.Get<Vector2>();
        }
    }
}
