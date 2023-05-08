
using System;
using UnityEngine;
using UnityEngine.InputSystem;

using Actor.Stats;
using Interface;
using StateMachine;

namespace Actor.Player
{
    // Values or methods that other can use
    public partial class Player
    {
        protected StateMachine<Player> StateMachine;
        public override void GetHit(DamageData data) => _GetHit(data);

        internal Vector2 Movement;
        internal Vector2 Stareing;

        internal Animator PlayerAnim;
        internal Rigidbody2D PlayerRigid;
        internal GameObject PlayerAttackCol;

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
            PlayerAttackCol = transform.GetChild(0).gameObject;
            PlayerAttackCol.gameObject.SetActive(false);
        }
        
        private void Start()
        {
            StateMachine = new StateMachine<Player>(this, new PlayerIdleState());
            StateMachine.AddState(new PlayerMoveState());
            StateMachine.AddState(new PlayerAttackState());
            StateMachine.AddState(new PlayerDiedState());
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
        private void _GetHit(DamageData data)
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
            if (!Movement.Equals(Vector2.zero))
            {
                Stareing = Movement;
            }
        }

        private void OnAutoAttack(InputValue value)
        {
            StateMachine.ChangeState<PlayerAttackState>();
        }

        private void OnSkill1(InputValue value)
        {
            StateMachine.ChangeState<PlayerDiedState>();
        }
    }
}
