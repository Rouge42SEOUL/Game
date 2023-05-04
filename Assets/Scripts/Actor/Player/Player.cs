
using Actor.Stats;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class Player
    {
        protected StateMachine<Player> StateMachine;
        public override void GetHit() => _GetHit();

        internal Vector2 Movement;

        internal Animator PlayerAnim;
        internal Rigidbody2D PlayerRigid;

        public PlayerStatObject Stat
        {
            get => _stat;
            private set
            {
                _stat = value;
            }
        }
    }
    
    // Values or methods that other cannot use
    public partial class Player
    {
        [SerializeField] private PlayerStatObject _stat;
        private PlayerInput _playerInput;
    }
    
    // body of MonoBehaviour
    public partial class Player : Actor
    {
        private void Awake()
        {
            PlayerAnim = GetComponent<Animator>();
            PlayerRigid = GetComponent<Rigidbody2D>();
        }
        
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
            Movement = value.Get<Vector2>();
        }

        private void OnAutoAttack(InputValue value)
        {
            StateMachine.ChangeState<PlayerAttackState>();
        }
    }
}
